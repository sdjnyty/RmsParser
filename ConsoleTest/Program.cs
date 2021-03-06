﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RmsParser;

namespace RmsParser.Test
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var syntaxTree = SyntaxTree.Parse(@"<PLAYER_SETUP>
  random_placement  /* note this is only currently valid entry */

/* ****************************************************** */
<LAND_GENERATION>

start_random
  percent_chance 50
  #define DESERT_MAP
end_random

if DESERT_MAP
  base_terrain                     DIRT
else           
  base_terrain                     GRASS3
endif

create_player_lands 
{ 
if DESERT_MAP
  terrain_type                     DIRT
else           
  terrain_type                     GRASS3
endif
  land_percent                       25
  base_size                           9
  border_fuzziness                   15
}

/* ****************************************************** */
<TERRAIN_GENERATION>

/* PRIMARY FOREST */

if DESERT_MAP
  create_terrain PALM_DESERT
{
  base_terrain                   DIRT
  spacing_to_other_terrain_types 5
  land_percent                   12
  number_of_clumps               10
  set_avoid_player_start_areas     
  set_scale_by_groups
}
else
create_terrain FOREST
{
  base_terrain                   GRASS3
  spacing_to_other_terrain_types 5
  land_percent                   12
  number_of_clumps               10
  set_avoid_player_start_areas     
  set_scale_by_groups
}
endif

/* PRIMARY PATCH */

if DESERT_MAP
   create_terrain DESERT
{
  base_terrain                   DIRT
  number_of_clumps               16
  spacing_to_other_terrain_types 0
  land_percent                   10
  set_scale_by_size
}
else
create_terrain DIRT
{
  base_terrain                   GRASS3
  number_of_clumps               26
  spacing_to_other_terrain_types 1
  land_percent                   9
  set_scale_by_size
}
endif

/* SECONDARY FOREST */

if DESERT_MAP
create_terrain FOREST
{
  base_terrain                   DIRT
  spacing_to_other_terrain_types 3
  land_percent                   1
  number_of_clumps               3
  set_avoid_player_start_areas     
  set_scale_by_groups
}
else
create_terrain PALM_DESERT
{
  base_terrain                   DIRT
  spacing_to_other_terrain_types 3
  land_percent                   1
  number_of_clumps               3
  set_avoid_player_start_areas     
  set_scale_by_groups
}
endif

/* SECONDARY PATCH */

if DESERT_MAP
   create_terrain DIRT3
{
  base_terrain                   DIRT
  number_of_clumps               24
  spacing_to_other_terrain_types 1
  land_percent                   2
  set_scale_by_size
}
else
create_terrain DIRT3
{
  base_terrain                   GRASS3
  number_of_clumps               24
  spacing_to_other_terrain_types 1
  land_percent                   2
  set_scale_by_size
}
endif

/* TERTIARY PATCH */

if DESERT_MAP
   create_terrain GRASS3
{
  base_terrain                   DIRT
  number_of_clumps               30
  spacing_to_other_terrain_types 1
  land_percent                   2
  set_scale_by_size
}
else
create_terrain DESERT
{
  base_terrain                   GRASS
  number_of_clumps               30
  spacing_to_other_terrain_types 1
  land_percent                   2
  set_scale_by_size
}
endif

/* OASES */

if DESERT_MAP
   create_terrain WATER
{
  base_terrain                   PALM_DESERT
  spacing_to_other_terrain_types 1
  land_percent                   1
  number_of_clumps               8
  set_avoid_player_start_areas
  set_flat_terrain_only     
  set_scale_by_groups
}
endif

/* ****************************************************** */
<OBJECTS_GENERATION>


create_object DEER
{
   number_of_objects 4
   group_variance 1
   set_loose_grouping
   set_gaia_object_only
   set_place_for_every_player
   min_distance_to_players    19
}

if DESERT_MAP
create_object PALMTREE
{
  number_of_objects          30
  set_gaia_object_only
  set_scaling_to_map_size
  min_distance_to_players    8
}
else
create_object OAKTREE
{
  number_of_objects          30
  set_gaia_object_only
  set_scaling_to_map_size
  min_distance_to_players    8
}
endif

if DESERT_MAP
create_object SHORE_FISH
{
  number_of_objects                    4
  min_distance_group_placement         5
  set_gaia_object_only
  set_scaling_to_map_size
}
endif

/* ****************************************************** */

<ELEVATION_GENERATION>

create_elevation        7
{
  if DESERT_MAP
  base_terrain                     DIRT
else           
  base_terrain                     GRASS3
endif
  number_of_clumps      20
  number_of_tiles       6000
  set_scale_by_groups
  set_scale_by_size           
}

/* ****************************************************** */
<CLIFF_GENERATION>
min_number_of_cliffs 10
max_number_of_cliffs 15
min_length_of_cliff  3
max_length_of_cliff  10

");
      Console.ReadLine();
    }
  }
}
