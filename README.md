# Procedural Generation Project
[![Made with Unity](https://img.shields.io/badge/Made%20with-Unity-57b9d3.svg?style=flat&logo=unity)](https://www.unity.com)

This is a **test project** on procedul generation algorithms.

<img src="https://github.com/xPoke-glitch/Procedural-Generation-Project/blob/main/Screenshots/basic-proc-gen.gif" width="750">

## The Game

It's a test project so there is not an actual objective.

You can try and see the different algorithms / scripts for procedural generation.

## Requirements

If you want to open, edit or see the Unity project:
* Unity 2020.3.6f1 (or greater)

# Procedural Generation Tests

1. **Basic procedural generation** - The script starts from a starting point and then it chooses randomly one of the 4 possible directions (up - down - left - right) till it reachs the max numbers of tiles set.
It also fills holes made of only 1 block and it adds lava/water all around the generated map.

<img src="https://github.com/xPoke-glitch/Procedural-Generation-Project/blob/main/Screenshots/basic-proc-gen.gif" width="750">

2. **Perlin noise map generation** - The map is generated based on Perlin noise

<img src="https://github.com/xPoke-glitch/Procedural-Generation-Project/blob/main/Screenshots/perlin-noise.gif" width="750">

It's interesting the result that you obtain by using tiles of different heights

<img src="https://github.com/xPoke-glitch/Procedural-Generation-Project/blob/main/Screenshots/perlin-heights.png" width="750">

3. **Mixed Basic procedural generation and Perlin noise** - I used the basic procedural generation to create a random map and then I used Perlin noise as height map, so that you can generate a map with tiles of different heights with some chunks of grass here and there

<img src="https://github.com/xPoke-glitch/Procedural-Generation-Project/blob/main/Screenshots/mixed-basic-perlin.gif" width="750">

4. **Maze Generation throught Randomized depth-first search** - I used this algorithm to create a maze generation in a separated scene. The maze is always different and there is always a path leading to an exit. Here you can see a 10x10 grid based maze

<img src="https://github.com/xPoke-glitch/Procedural-Generation-Project/blob/main/Screenshots/maze-gen.gif" width="750">

<img src="https://github.com/xPoke-glitch/Procedural-Generation-Project/blob/main/Screenshots/maze-in.gif" width="750">

5. **Conway Game of Life** - I created the Conway Game of Life in order to understand and use it for procedural generation (see point 6). You can draw on the board by clicking each cell, right click to fill an empty space and press play button to start/stop the simulation

<img src="https://github.com/xPoke-glitch/Procedural-Generation-Project/blob/main/Screenshots/conway-gol.gif" width="750">

6. **Cellular Automata Generation** - Generate a map using a random sailor pass and smoothing it with multiple iterations of the Conway Game of Life algorithm

<img src="https://github.com/xPoke-glitch/Procedural-Generation-Project/blob/main/Screenshots/cellular-automata.gif" width="750">

## Team

**Developers**:
* Cristian: https://github.com/xPoke-glitch / https://pokedev.itch.io/
