goog.provide('game.board');
goog.require('game.pieces');

/*
 * Width of each of the two lines that delimit the board
 * @type {number}
 */
game.board.BOARD_LINE_WIDTH = 6;
game.board.BLOCK_SIZE = 16; 	 // Width and Height of each block of a piece
game.board.BOARD_POSITION = 320;	 // Center position of the board from the left of the screen
game.board.BOARD_WIDTH = 10;	 // Board width in blocks
game.board.BOARD_HEIGHT = 20;	 // Board height in blocks
game.board.MIN_VERTICAL_MARGIN = 20;	 // Minimum vertical margin for the board limit
game.board.MIN_HORIZONTAL_MARGIN = 20;	// Minimum horizontal margin for the board limit
game.board.PIECE_BLOCKS = 5;  // Number of horizontal and vertical blocks of a matrix piece

game.board.posEnum = {
	POS_FREE: 0,
	POS_FILLED: 1
}

game.board.board = [[]];

game.board.initBoard = function () {
	game.board.length = this.BOARD_WIDTH;
	var i = 0, j = 0;
	for (i = 0; i < this.BOARD_WIDTH; i++) {
		this.board[i] = [];
		for (j = 0; j < this.BOARD_HEIGHT; j++)
			this.board[i][j] = this.posEnum.POS_FREE;
	}
}