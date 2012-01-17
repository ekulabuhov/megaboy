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
};

game.board.board = [[]];
game.board.screenHeight = 0;

game.Board = function (screenHeight) {
	game.board.screenHeight = screenHeight;
	//game.initBoard();
}

game.Board.prototype.initBoard = function () {
	return window.height;
}

/**
* Init the board blocks with free positions
*/
game.board.initBoard = function () {
	game.board.length = this.BOARD_WIDTH;
	var i = 0, j = 0;
	for (i = 0; i < this.BOARD_WIDTH; i++) {
		this.board[i] = [];
		for (j = 0; j < this.BOARD_HEIGHT; j++)
			this.board[i][j] = this.posEnum.POS_FREE;
	}
};

/**
* Store a piece in the board by filling the blocks
*/
game.board.storePiece = function (x, y, piece, rotation) {
	for (var i1 = x, i2 = 0; i1 < x + this.PIECE_BLOCKS; i1++, i2++)
		for (var j1 = y, j2 = 0; j1 < y + this.PIECE_BLOCKS; j1++, j2++) {
			if (game.pieces.getBlockType(piece, rotation, j2, i2) != 0)
				this.board[i1][j1] = this.posEnum.POS_FILLED;
		}
};

/**
* Check if the game is over because a piece have achieved the upper position
*/
game.board.isGameOver = function () {
	for (var i = 0; i < this.BOARD_WIDTH; i++)
		if (this.board[i][0] == this.posEnum.POS_FILLED) return true;

	return false;
};

/**
* Delete a line of the board by moving all above lines down
*/
game.board.deleteLine = function (y) {
	// Moves all the upper lines one row down
	for (var j = y; j > 0; j--)
		for (var i = 0; i < this.BOARD_WIDTH; i++)
			this.board[i][j] = this.board[i][j - 1];
};

game.board.deletePossibleLines = function () {
	for (var j = 0; j < this.BOARD_HEIGHT; j++) {
		var i = 0;
		while (i < this.BOARD_WIDTH) {
			if (mBoard[i][j] != this.posEnum.POS_FILLED) break;
			i++;
		}

		if (i == this.BOARD_WIDTH) this.DeleteLine(j);
	}
};

game.board.isFreeBlock = function (x, y) {
	if (this.board[x][y] == this.posEnum.POS_FREE) return true; else return false;
};

game.board.getXPosInPixels = function (pos) {
	return ((this.BOARD_POSITION - (this.BLOCK_SIZE * (this.BOARD_WIDTH / 2))) + (pos * this.BLOCK_SIZE));
};

game.board.getYPosInPixels = function (pos) {
	return ((mScreenHeight - (this.BLOCK_SIZE * this.BOARD_HEIGHT)) + (pos * this.BLOCK_SIZE));
};

game.board.isPossibleMovement = function (x, y, piece, rotation) {
	// Checks collision with pieces already stored in the board or the board limits
	// This is just to check the 5x5 blocks of a piece with the appropriate area in the board
	for (var i1 = x, i2 = 0; i1 < x + this.PIECE_BLOCKS; i1++, i2++) {
		for (var j1 = y, j2 = 0; j1 < y + this.PIECE_BLOCKS; j1++, j2++) {
			// Check if the piece is outside the limits of the board
			if (i1 < 0 || i1 > this.BOARD_WIDTH - 1 || j1 > this.BOARD_HEIGHT - 1) {
				if (game.pieces.getBlockType(piece, rotation, j2, i2) != 0)
					return 0;
			}

			// Check if the piece have collisioned with a block already stored in the map
			if (j1 >= 0) {
				if ((game.pieces.getBlockType(piece, rotation, j2, i2) != 0) && (!this.isFreeBlock(i1, j1)))
					return false;
			}
		}
	}

	// No collision
	return true;
};