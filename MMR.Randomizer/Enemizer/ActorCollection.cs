using MMR.Common.Extensions;
using MMR.Randomizer.Models.Rom;
using MMR.Randomizer.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMR.Randomizer.Enemizer
{
    // for now, this file keeps all actorizer data load book keeping, should probably split again

    public class BaseEnemiesCollection
    {
        // sum of overlay code per actortype in this collection
        public int OverlayRamSize;
        // sum of all enemy instances struct ram requirements
        public int ActorInstanceSum;
        // sum of object size
        public List<int> ObjectList;
        public int ObjectRamSize;
        public int DynaPolySize;
        public int DynaVertSize;
        public int[] objectSizes; //debug
        // list of enemies that were used to make this
        public List<Actor> oldActorList = null;

        public BaseEnemiesCollection(List<Actor> actorList, List<int> objList, Scene s)
        {
            /// values per day/night

            oldActorList = actorList;
            var distinctActors = actorList.DistinctBy(act => act.ActorId);
            OverlayRamSize = distinctActors.Select(x => ActorUtils.GetOvlCodeRamSize(x.ActorId)).Sum();
            ActorInstanceSum = actorList.Select(act => act.ActorId)
                                        .Select(act => ActorUtils.GetOvlInstanceRamSize(act, Enemies.InjectedActors)).Sum();
            this.ObjectList = objList;
            this.objectSizes = objList.Select(x => ObjUtils.GetObjSize(x)).ToArray();
            this.ObjectRamSize = objectSizes.Sum();

            this.CalculateDefaultObjectUse(s);

            this.UpdateDynaLoad(actorList);
        }

        public void UpdateDynaLoad(List<Actor> actorList)
        {
            this.DynaPolySize = 0;
            this.DynaVertSize = 0;
            for (int act = 0; act < actorList.Count; act++)
            {
                var actor = actorList[act];

                this.DynaPolySize += actor.DynaLoad.poly;
                this.DynaVertSize += actor.DynaLoad.vert;
            }
        }

        public void CalculateDefaultObjectUse(Scene s)
        {
            // now that we know the hard object bank limits, we need ALL data
            // in addition to the scene objects, we need the objects that are always loaded
            this.ObjectList.Append(1);
            this.ObjectRamSize += 0x925E0; // gameplay_keep
            this.ObjectList.Append(0x11);
            this.ObjectRamSize += 0x1E250; // the biggest link form object (child)
            // scenes can have special scene objects, which arent included in actor objects
            if (s.SpecialObject == Scene.SceneSpecialObject.FieldKeep)
            {
                this.ObjectList.Append(0x2);
                this.ObjectRamSize += 0x9290; // field keep object
                /// I still dont know why epona sometimes spawns before the objects from scene are loaded, assumption its field
                if (s.SceneEnum != GameObjects.Scene.IkanaCanyon)
                {
                    this.ObjectList.Append(0x7D);
                    this.ObjectRamSize += 0xE4F0; // epona
                }
            }
            else if (s.SpecialObject == Scene.SceneSpecialObject.DungeonKeep)
            {
                this.ObjectList.Append(0x3);
                this.ObjectRamSize += 0x23280;
            }
        }
    } // end BaseEnemiesCollection

    public class MapEnemiesCollection
    {
        public BaseEnemiesCollection day = null;
        public BaseEnemiesCollection night = null;

        public MapEnemiesCollection(List<Actor> actorList, List<int> objList, Scene scene)
        {
            // split enemies into day and night, init two types
            int dayFlagMask = 0x2AA; // nigth is just shifted to the right by one

            var dayActors = actorList.FindAll(act => (act.GetTimeFlags() & dayFlagMask) > 0);
            this.day = new BaseEnemiesCollection(dayActors, objList, scene);
            var nightActors = actorList.FindAll(act => (act.GetTimeFlags() & (dayFlagMask >> 1)) > 0);
            this.night = new BaseEnemiesCollection(nightActors, objList, scene);
            #if DEBUG
            //var missingElements = actorList.Except(dayActors).Except(nightActors).ToList();
            //Debug.Assert(missingElements.Count == 0);
            #endif
        }
    }

    public class ActorsCollection
    {
        /// Data class to keep track of size of replacment parts in enemizer
        /// Actors (ram overlay size, ram instance size), Objects (ram size), Dyna load
        
        // per scene:
        //   per old and new:
        //     per room :
        //       per night and day:
        //         an object size, an actor inst size, and a actor code size
        // for each scene we need to check all of them, this is getting complicated

        public List<MapEnemiesCollection> oldMapList;
        public List<MapEnemiesCollection> newMapList;
        public Scene Scene;
        public int sceneObjectLimit;


        public ActorsCollection(Scene scene)
        {
            this.Scene = scene;
            this.oldMapList = new List<MapEnemiesCollection>();
            this.sceneObjectLimit = SceneUtils.GetSceneObjectBankSize(scene.SceneEnum);
            for (int i = 0; i < scene.Maps.Count; ++i)
            {
                var map = scene.Maps[i];
                this.oldMapList.Add(new MapEnemiesCollection(map.Actors, map.Objects, scene));
            }
        }

        public void SetNewActors(Scene scene, List<List<int>> newObjects)
        {
            // init for new replacements
            // this doesnt set actors anywhere tho, just objects, misnomer?

            this.newMapList = new List<MapEnemiesCollection>();
            // I like foreach better but its waaaay slower
            for (int m = 0; m < scene.Maps.Count; ++m)
            {
                var map = scene.Maps[m];

                var newObjList = newObjects[m];
                this.newMapList.Add(new MapEnemiesCollection(map.Actors, newObjList, scene));
            }
        }

        public List<List<Actor>> GenerateShrinkableDynaList()
        {
            List<List<Actor>> shrinkableActorList = new List<List<Actor>>();

            for (int m = 0; m < this.newMapList.Count; m++)
            {
                var map = this.newMapList[m];

                // compare headroom to actual
                if (isDynaOverLoaded(map.day, this.oldMapList[m].day, m))
                {
                    shrinkableActorList = buildDynaShrinkableListPerMap(map.day.oldActorList);
                }
                if (isDynaOverLoaded(map.night, this.oldMapList[m].night, m))
                {
                    shrinkableActorList = buildDynaShrinkableListPerMap(map.night.oldActorList);
                }
            }

            return shrinkableActorList;
        }

        public bool isDynaOverLoaded(BaseEnemiesCollection newCollection, BaseEnemiesCollection oldCollection, int mapIndex)
        {
            var dynaHeadroomAttr = SceneUtils.GetSceneDynaAttributes(this.Scene.SceneEnum, mapIndex);
            if (dynaHeadroomAttr != null)
            {
                var dayPolyDiff = newCollection.DynaPolySize - oldCollection.DynaPolySize;
                var dayVertDiff = newCollection.DynaVertSize - oldCollection.DynaVertSize;
                return (dayPolyDiff > dynaHeadroomAttr.Polygon || dayVertDiff > dynaHeadroomAttr.Verticies);
            }
            return false; // not considered dyna limited
        }

        private List<List<Actor>> buildDynaShrinkableListPerMap(List<Actor> actorList)
        {
            // this is run per night or day

            // generate a list of groups of actors, such that every list has only actors of the same ID
            // IE: list 1 is elevators, list 2 is deku flowers
            // we trim all groups that only have one actor, as those are not trimable
            return actorList
                        .Where(a => a.DynaLoad.poly > 0 && (int)a.OldActorEnum != a.ActorId)
                        .GroupBy(a => a.ActorId)
                        .Where(g => g.Count() > 1)
                        .Select(g => g.ToList())
                        .ToList();            
        }

        private bool testDynaSize()
        {

            for (int m = 0; m < oldMapList.Count; ++m)
            {
                if (isDynaOverLoaded(this.newMapList[m].day, this.oldMapList[m].day, m))
                    return false;
                if (isDynaOverLoaded(this.newMapList[m].night, this.oldMapList[m].night, m))
                    return false;
            }

            return true;
        }

        public bool isSizeAcceptable(StringBuilder log)
        {
            // is the overall size for all maps of night and day equal

            var objectTest = isObjectSizeAcceptable();
            if (objectTest > 0)
            {
                log.AppendLine($" ---- bogo REJECTED: objects are too big (by {objectTest})" +
                    $"\n [{string.Join(",", this.newMapList[0].day.ObjectList)}]" +
                    $"\n [{string.Join(",", this.newMapList[0].day.objectSizes)}");
                return false;
            }

            var dynatest = testDynaSize();
            if (dynatest == false)
            {
                log.AppendLine($" ---- bogo REJECTED: dyna actors are too big, even after trim");
                return false;
            }

            for (int map = 0; map < oldMapList.Count; ++map) // per map
            {
                // pos diff is smaller
                var sizeTest = CompareRamRequirements(this.Scene, oldMapList[map].day, newMapList[map].day);
                if (sizeTest == false)
                {
                    log.AppendLine($" ---- bogo REJECTED: map {map} does not meed RAM requirements for DAY");
                    return false;
                }

                sizeTest = CompareRamRequirements(this.Scene, oldMapList[map].night, newMapList[map].night);
                if (sizeTest == false)
                {
                    log.AppendLine($" ---- bogo REJECTED: map {map} does not meed RAM requirements for NIGHT");
                    return false;
                }

                // compare dyna requirements

            }
            return true; // all of them passed size test
        }

        public bool CompareRamRequirements(Scene scene, BaseEnemiesCollection oldCollection, BaseEnemiesCollection newCollection)
        {
            var dayOvlDiff = oldCollection.OverlayRamSize - newCollection.OverlayRamSize;
            var dayInstDiff = oldCollection.ActorInstanceSum - newCollection.ActorInstanceSum;

            // if the new size is smaller than the old size we should be dandy, if not...
            if (dayOvlDiff + dayInstDiff <= -0x100)
            {
                if (scene.SceneEnum == GameObjects.Scene.IkanaCanyon
                    && (newCollection.OverlayRamSize + newCollection.ActorInstanceSum > 0x64FFF)) // trying a bit higher for ikana canyon
                {
                    return false;
                }

                // SCT is 0x4FF90
                else if (newCollection.OverlayRamSize + newCollection.ActorInstanceSum > 0x4FFFF) // need to find new safe values
                {
                    return false;
                }
                // I can't rule out halucination scrubs are or are not the issue, their skeleton->action is broken, that sounds like corrupted heap
                if (scene.SceneEnum == GameObjects.Scene.DekuPalace
                    && (newCollection.OverlayRamSize + newCollection.ActorInstanceSum > 0x22000)) // need to find new safe values
                {
                    return false;
                }
            }

            return true;
        }

        public int isObjectSizeAcceptable(List<int> objects = null)
        {
            /// checks if the object load of the current object list will blow out the object space

            for (int map = 0; map < oldMapList.Count; ++map)
            {
                /* if (newMapList[map].day.ObjectRamSize > sceneObjectLimit || newMapList[map].night.ObjectRamSize > sceneObjectLimit)
                {
                    return false;
                }// */
                //var oldObjectSize = this.oldMapList[map].day.ObjectList.Sum();//this.ObjectList.Sum();
                int newObjectSize;
                if (objects != null)
                {
                    newObjectSize = objects.Sum();
                }
                else
                {
                    newObjectSize = this.newMapList[map].day.ObjectRamSize;
                }

                if (newObjectSize > this.sceneObjectLimit)
                {
                    return (newObjectSize - this.sceneObjectLimit);
                }
            }

            return 0;
        }

        public string isDynaSizeAcceptable()
        {
            //return "acceptable"; // temp testing
            for (int map = 0; map < oldMapList.Count; ++map)
            {
                // pull dynaheadroom for the scene, if there isnt one continue
                var dynaHeadroomAttr = SceneUtils.GetSceneDynaAttributes(this.Scene.SceneEnum, map);
                if (dynaHeadroomAttr == null) continue; // this room has none

                // compare headroom to actual
                var dayPolyDiff = this.newMapList[map].day.DynaPolySize - this.oldMapList[map].day.DynaPolySize;
                if (dayPolyDiff > dynaHeadroomAttr.Polygon)
                {
                    return $"map [{map}] day poly: [{dayPolyDiff}]";
                }

                var dayVertDiff = this.newMapList[map].day.DynaVertSize - this.oldMapList[map].day.DynaVertSize;
                if (dayVertDiff > dynaHeadroomAttr.Verticies)
                {
                    return $"map [{map}] day vert: [{dayVertDiff}]";
                }

                var nightPolyDiff = this.newMapList[map].night.DynaPolySize - this.oldMapList[map].night.DynaPolySize;
                if (nightPolyDiff > dynaHeadroomAttr.Polygon)
                {
                    return $"map [{map}] day poly: [{nightPolyDiff}]";
                }

                var nightVertDiff = this.newMapList[map].night.DynaVertSize - this.oldMapList[map].night.DynaVertSize;
                if (nightVertDiff > dynaHeadroomAttr.Verticies)
                {
                    return $"map [{map}] day vert: [{nightVertDiff}]";
                }
            }

            return "acceptable";
        }


        // print to log function
        public void PrintAllMapRamObjectOutput(StringBuilder log)
        {
            void PrintCombineRatioNewOld(string text, int newv, int oldv)
            {
                log.AppendLine(text + " ratio: [" + ((float)newv / (float)oldv).ToString("F4")
                    + "] newsize: [" + newv.ToString("X6") + "] oldsize: [" + oldv.ToString("X6") + "]");
            }
            void PrintCombineDeltaNewOld(string text, int newv, int oldv)
            {
                log.AppendLine(text + " delta: [" + (newv - oldv).ToString()
                    + "] newsize: [" + newv.ToString("X6") + "] oldsize: [" + oldv.ToString("X6") + "]");
            }

            if (newMapList == null)
            {
                log.AppendLine(" ERROR: New list was dead!");
                return;
            }

            for (int map = 0; map < oldMapList.Count; ++map) // per map
            {
                var newDTotal = newMapList[map].day.OverlayRamSize + newMapList[map].day.ActorInstanceSum;
                var oldDTotal = oldMapList[map].day.OverlayRamSize + oldMapList[map].day.ActorInstanceSum;
                var newNTotal = newMapList[map].night.OverlayRamSize + newMapList[map].night.ActorInstanceSum;
                var oldNTotal = oldMapList[map].night.OverlayRamSize + oldMapList[map].night.ActorInstanceSum;

                // PRINT EVERYTHING
                //if (newDTotal - oldDTotal + newNTotal - oldNTotal == 0) continue; // map was untouched, dont print

                log.AppendLine(" ======( Map " + map.ToString("X2") + " )======");

                PrintCombineRatioNewOld("  day:    overlay ", newMapList[map].day.OverlayRamSize, oldMapList[map].day.OverlayRamSize);
                PrintCombineRatioNewOld("  day:    struct  ", newMapList[map].day.ActorInstanceSum, oldMapList[map].day.ActorInstanceSum);
                PrintCombineRatioNewOld("  day:    total  =", newDTotal, oldDTotal);

                PrintCombineRatioNewOld("  night:  overlay ", newMapList[map].night.OverlayRamSize, oldMapList[map].night.OverlayRamSize);
                PrintCombineRatioNewOld("  night:  struct  ", newMapList[map].night.ActorInstanceSum, oldMapList[map].night.ActorInstanceSum);
                PrintCombineRatioNewOld("  night:  total  =", newNTotal, oldNTotal);

                log.AppendLine($"  ------------------------------------------------------ ");

                PrintCombineRatioNewOld("  day:    object  ", newMapList[map].day.ObjectRamSize, oldMapList[map].day.ObjectRamSize);
                PrintCombineRatioNewOld("  night:  object  ", newMapList[map].night.ObjectRamSize, oldMapList[map].night.ObjectRamSize);


                // print map objects size
                var hexString = "";
                for (int i = 0; i < newMapList[map].day.objectSizes.Length; i++)
                {
                    hexString += "0x" + newMapList[map].day.objectSizes[i].ToString("X") + " ";
                }
                var size = newMapList[map].day.objectSizes.Sum().ToString("X");
                var allSize = newMapList[map].day.ObjectRamSize.ToString("X");
                log.AppendLine($"   object sizes: [ {hexString}]");
                log.AppendLine($"    sum: [0x{size}] allsize: [0x{allSize}]");
                log.AppendLine($"  ------------------------------------------------------ ");

                PrintCombineDeltaNewOld("  day:    dyna poly  ", newMapList[map].day.DynaPolySize, oldMapList[map].day.DynaPolySize);
                PrintCombineDeltaNewOld("  day:    dyna vert  ", newMapList[map].day.DynaVertSize, oldMapList[map].day.DynaVertSize);
                PrintCombineDeltaNewOld("  night:  dyna poly  ", newMapList[map].night.DynaPolySize, oldMapList[map].night.DynaPolySize);
                PrintCombineDeltaNewOld("  night:  dyna vert  ", newMapList[map].night.DynaVertSize, oldMapList[map].night.DynaVertSize);

                log.AppendLine($" ------------------------------------------------- ");
            }
        } // end PrintAllMapRamObjectOutput
    } // end actorsCollection
}
