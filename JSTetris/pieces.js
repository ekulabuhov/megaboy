goog.provide('game.pieces');

/**
 * Return the type of a block (0 = no-block, 1 = normal block, 2 = pivot block)
 *
 * @param {number} piece Piece to draw
 * @param {number} rotation 1 of the 4 possible rotations
 * @param {number} x Horizontal position in blocks
 * @param {number} y Vertical position in blocks
 * @return {number} Return the type of a block (0 = no-block, 1 = normal block, 2 = pivot block)
 */
game.pieces.getBlockType = function (piece, rotation, x, y) {
	return game.pieces.pieces[piece][rotation][x][y];
};

/**
* Returns the horizontal displacement of the piece that has to be applied in order to create it in the
* correct position.
*
* @param {number} piece Piece to draw
* @param {number} rotation 1 of the 4 possible rotations
* @return {number} Returns the horizontal displacement
*/
game.pieces.getXInitialPosition = function (piece, rotation) {
	return game.pieces.initialPosition[piece][rotation][0];
};

/**
* Returns the vertical displacement of the piece that has to be applied in order to create it in the
* correct position.
*
* @param {number} piece Piece to draw
* @param {number} rotation 1 of the 4 possible rotations
* @return {number} Returns the vertical  displacement
*/
game.pieces.getYInitialPosition = function (piece, rotation) {
	return game.pieces.initialPosition[piece][rotation][1];
};

game.pieces.pieces =
[
// Square
[
[
[0, 0, 0, 0, 0],
[0, 0, 0, 0, 0],
[0, 0, 2, 1, 0],
[0, 0, 1, 1, 0],
[0, 0, 0, 0, 0]
],
[
[0, 0, 0, 0, 0],
[0, 0, 0, 0, 0],
[0, 0, 2, 1, 0],
[0, 0, 1, 1, 0],
[0, 0, 0, 0, 0]
],
[
[0, 0, 0, 0, 0],
[0, 0, 0, 0, 0],
[0, 0, 2, 1, 0],
[0, 0, 1, 1, 0],
[0, 0, 0, 0, 0]
],
[
[0, 0, 0, 0, 0],
[0, 0, 0, 0, 0],
[0, 0, 2, 1, 0],
[0, 0, 1, 1, 0],
[0, 0, 0, 0, 0]
]
],

// I
[
[
[0, 0, 0, 0, 0],
[0, 0, 0, 0, 0],
[0, 1, 2, 1, 1],
[0, 0, 0, 0, 0],
[0, 0, 0, 0, 0]
],
[
[0, 0, 0, 0, 0],
[0, 0, 1, 0, 0],
[0, 0, 2, 0, 0],
[0, 0, 1, 0, 0],
[0, 0, 1, 0, 0]
],
[
[0, 0, 0, 0, 0],
[0, 0, 0, 0, 0],
[1, 1, 2, 1, 0],
[0, 0, 0, 0, 0],
[0, 0, 0, 0, 0]
],
[
[0, 0, 1, 0, 0],
[0, 0, 1, 0, 0],
[0, 0, 2, 0, 0],
[0, 0, 1, 0, 0],
[0, 0, 0, 0, 0]
]
]
,
// L
[
[
[0, 0, 0, 0, 0],
[0, 0, 1, 0, 0],
[0, 0, 2, 0, 0],
[0, 0, 1, 1, 0],
[0, 0, 0, 0, 0]
],
[
[0, 0, 0, 0, 0],
[0, 0, 0, 0, 0],
[0, 1, 2, 1, 0],
[0, 1, 0, 0, 0],
[0, 0, 0, 0, 0]
],
[
[0, 0, 0, 0, 0],
[0, 1, 1, 0, 0],
[0, 0, 2, 0, 0],
[0, 0, 1, 0, 0],
[0, 0, 0, 0, 0]
],
[
[0, 0, 0, 0, 0],
[0, 0, 0, 1, 0],
[0, 1, 2, 1, 0],
[0, 0, 0, 0, 0],
[0, 0, 0, 0, 0]
]
],
// L mirrored
[
[
[0, 0, 0, 0, 0],
[0, 0, 1, 0, 0],
[0, 0, 2, 0, 0],
[0, 1, 1, 0, 0],
[0, 0, 0, 0, 0]
],
[
[0, 0, 0, 0, 0],
[0, 1, 0, 0, 0],
[0, 1, 2, 1, 0],
[0, 0, 0, 0, 0],
[0, 0, 0, 0, 0]
],
[
[0, 0, 0, 0, 0],
[0, 0, 1, 1, 0],
[0, 0, 2, 0, 0],
[0, 0, 1, 0, 0],
[0, 0, 0, 0, 0]
],
[
[0, 0, 0, 0, 0],
[0, 0, 0, 0, 0],
[0, 1, 2, 1, 0],
[0, 0, 0, 1, 0],
[0, 0, 0, 0, 0]
]
],
// N
[
[
[0, 0, 0, 0, 0],
[0, 0, 0, 1, 0],
[0, 0, 2, 1, 0],
[0, 0, 1, 0, 0],
[0, 0, 0, 0, 0]
],
[
[0, 0, 0, 0, 0],
[0, 0, 0, 0, 0],
[0, 1, 2, 0, 0],
[0, 0, 1, 1, 0],
[0, 0, 0, 0, 0]
],
[
[0, 0, 0, 0, 0],
[0, 0, 1, 0, 0],
[0, 1, 2, 0, 0],
[0, 1, 0, 0, 0],
[0, 0, 0, 0, 0]
],

[
[0, 0, 0, 0, 0],
[0, 1, 1, 0, 0],
[0, 0, 2, 1, 0],
[0, 0, 0, 0, 0],
[0, 0, 0, 0, 0]
]
],
// N mirrored
[
[
[0, 0, 0, 0, 0],
[0, 0, 1, 0, 0],
[0, 0, 2, 1, 0],
[0, 0, 0, 1, 0],
[0, 0, 0, 0, 0]
],
[
[0, 0, 0, 0, 0],
[0, 0, 0, 0, 0],
[0, 0, 2, 1, 0],
[0, 1, 1, 0, 0],
[0, 0, 0, 0, 0]
],
[
[0, 0, 0, 0, 0],
[0, 1, 0, 0, 0],
[0, 1, 2, 0, 0],
[0, 0, 1, 0, 0],
[0, 0, 0, 0, 0]
],
[
[0, 0, 0, 0, 0],
[0, 0, 1, 1, 0],
[0, 1, 2, 0, 0],
[0, 0, 0, 0, 0],
[0, 0, 0, 0, 0]
]
],
// T
[
[
[0, 0, 0, 0, 0],
[0, 0, 1, 0, 0],
[0, 0, 2, 1, 0],
[0, 0, 1, 0, 0],
[0, 0, 0, 0, 0]
],
[
[0, 0, 0, 0, 0],
[0, 0, 0, 0, 0],
[0, 1, 2, 1, 0],
[0, 0, 1, 0, 0],
[0, 0, 0, 0, 0]
],
[
[0, 0, 0, 0, 0],
[0, 0, 1, 0, 0],
[0, 1, 2, 0, 0],
[0, 0, 1, 0, 0],
[0, 0, 0, 0, 0]
],
[
[0, 0, 0, 0, 0],
[0, 0, 1, 0, 0],
[0, 1, 2, 1, 0],
[0, 0, 0, 0, 0],
[0, 0, 0, 0, 0]
]
]
];

game.pieces.initialPosition =
[
/* Square */
[
[-2, -3],
[-2, -3],
[-2, -3],
[-2, -3]
],
/* I */
[
[-2, -2],
[-2, -3],
[-2, -2],
[-2, -3]
],
/* L */
[
[-2, -3],
[-2, -3],
[-2, -3],
[-2, -2]
],
/* L mirrored */
[
[-2, -3],
[-2, -2],
[-2, -3],
[-2, -3]
],
/* N */
[
[-2, -3],
[-2, -3],
[-2, -3],
[-2, -2]
],
/* N mirrored */
[
[-2, -3],
[-2, -3],
[-2, -3],
[-2, -2]
],
/* T */
[
[-2, -3],
[-2, -3],
[-2, -3],
[-2, -2]
],
];