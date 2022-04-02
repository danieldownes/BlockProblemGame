# Block Problem

I was gifted a wooden puzzle game that contains an grid array of 4x4 blocks.

I've never since seen this puzzle game implemented.

Based on a 4x4 grid of wooden blocks, each block has a graphic on its top-side.
Each graphic indicates a colour for each edge of the square.  
Half the blocks also contain a diamond in the centre. The other half do not.

Level 1 challenge is that:
The player must place all the blocks so that adjacent blocks  have matching edge colours.

Level 2 challenge:
Level 1 requirement plus:
No two blocks containing diamond should touch, eg, blocks with diamonds  should be laid out
in checker style pattern with respect to blocks without diamonds.

## Visual Basic Implementation

I implemented the blocks including data to hold the 4 colours and set the diamond flag.

The UI allows the user to swap two blocks with two clicks. And also rotate a block with a right-click.

![image](https://user-images.githubusercontent.com/1720388/161404512-a0371604-c718-492d-84f1-433117ad880d.png)
