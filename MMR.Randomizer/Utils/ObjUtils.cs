using MMR.Randomizer.Models.Rom;

namespace MMR.Randomizer.Utils
{

    public static class ObjUtils
    {
        const int OBJECT_TABLE = 0xC58C80;
        public static int GetObjSize(int objTableIndex)
        {
            /// object size already exists in the object table, just look it up from the object index
            int objTableFID = RomUtils.GetFileIndexForWriting(OBJECT_TABLE);
            // the object table exists inside of another file, we need the offset to the table
            int objTableOffset = OBJECT_TABLE - RomData.MMFileList[objTableFID].Addr;
            var objTableData = RomData.MMFileList[objTableFID].Data;
            return (int)(ReadWriteUtils.Arr_ReadU32(objTableData, objTableOffset + (objTableIndex * 8) + 4)
                       - ReadWriteUtils.Arr_ReadU32(objTableData, objTableOffset + (objTableIndex * 8)));
        }

        public static byte[] GetObjectData(int objectIndex)
        {
            var objectTableFileIndex = RomUtils.GetFileIndexForWriting(OBJECT_TABLE);
            var baseAddress = OBJECT_TABLE - RomData.MMFileList[objectTableFileIndex].Addr;
            var objectAddress = ReadWriteUtils.Arr_ReadU32(RomData.MMFileList[objectTableFileIndex].Data, baseAddress + (objectIndex * 8));
            var objectFileIndex = RomData.MMFileList.FindIndex(f => f.Addr == objectAddress);
            if (objectFileIndex == -1)
            {
                return null;
            }
            RomUtils.CheckCompressed(objectFileIndex);
            return RomData.MMFileList[objectFileIndex].Data;
        }

        public static void InsertIndexedObj(int index, int replace, params byte[][] obj)
        {
            InsertObj(obj[index], replace);
        }

        public static void InsertObj(byte[] obj, int replace)
        {
            int f = RomUtils.GetFileIndexForWriting(OBJECT_TABLE);
            int basea = OBJECT_TABLE - RomData.MMFileList[f].Addr;
            uint replaceaddr = ReadWriteUtils.Arr_ReadU32(RomData.MMFileList[f].Data, basea + (replace * 8));
            int objf = RomData.MMFileList.FindIndex(u => u.Addr == replaceaddr);
            if (objf == -1)
            {
                return;
            };
            if (obj.Length > (RomData.MMFileList[objf].End - RomData.MMFileList[objf].Addr))
            {
                MMFile newfile = new MMFile();
                newfile.Addr = RomData.MMFileList[RomData.MMFileList.Count - 1].End;
                newfile.End = newfile.Addr + obj.Length;
                newfile.IsCompressed = true;
                newfile.WasEdited = true;
                newfile.Data = obj;
                RomData.MMFileList[objf].Cmp_Addr = -1;
                RomData.MMFileList[objf].Cmp_End = -1;
                RomData.MMFileList[objf].Data = null;
                RomData.MMFileList[objf].IsCompressed = false;
                RomData.MMFileList.Add(newfile);
                ReadWriteUtils.Arr_WriteU32(RomData.MMFileList[f].Data, basea + (replace * 8), (uint)newfile.Addr);
                ReadWriteUtils.Arr_WriteU32(RomData.MMFileList[f].Data, basea + (replace * 8) + 4, (uint)newfile.End);
            }
            else
            {
                RomData.MMFileList[objf].Data = obj;
                RomData.MMFileList[objf].WasEdited = true;
            }
        }
    }

}