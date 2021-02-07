#include "save_file.h"

/**************************
 * Called from Savedata_after_load hook function
 * TODO:  rename this function to match cats new function naming system
 *************************/
void set_starting_egg_count(){
  // after save is loaded, set egg count at inverse of required egg count
  
  // for now static because tying in a MMR.Randomizer variable is complicated
  u8 egg_req = 4;
  u8 starting_eggs = 7 - egg_req;
  u8 * egg_count_RAM_location = (u8  *)0x801EFCA3;
  *egg_count_RAM_location = starting_eggs;
  // wait its probably in save context, but what offset?

}


