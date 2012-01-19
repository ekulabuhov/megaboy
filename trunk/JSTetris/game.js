goog.provide('game');
goog.require('game.pieces');
goog.require('game.board');

game.WAIT_TIME = 700; 	 // Number of milliseconds that the piece remains before going 1 block down */

game.Color = {
	BLACK: 0,
	RED: 1,
	GREEN: 2,
	BLUE: 3,
	CYAN: 4,
	MAGENTA: 5,
	YELLOW: 6,
	WHITE: 7,
	COLOR_MAX: 8
};

/*
 * Get a random int between to integers
 */
game.getRand = function (a, b) {
	return Math.floor(Math.random() * b + a);
};

/*
 * InitGame, takes care of the initialization of the game by selecting the first and next piece randomly. 
 * The next piece is shown so the player can see which piece will appear next.
 * This method also sets the position in blocks of that pieces.
 * We use two methods that we have seen before in "Pieces" class: GetXInitialPosition and GetYInitialPosition in order to initialize the piece in the correct position.
 */
game.initGame = function () {
	// Init random numbers
	//srand ((unsigned int) time(NULL));

	// First piece
	game.piece	 = this.getRand (0, 6);
	game.rotation	 = this.getRand (0, 3);
	game.posX = (this.board.BOARD_WIDTH / 2) + this.pieces.getXInitialPosition (game.piece, game.rotation);
	game.posY = this.pieces.getYInitialPosition (game.piece, game.rotation);

	// Next piece
	game.nextPiece_ = this.getRand (0, 6);
	game.nextRotation_ = this.getRand (0, 3);
	game.nextPosX_ = this.board.BOARD_WIDTH + 5;
	game.nextPosY_ = 5;
};

/*
 * CreateNewPiece method sets the "next piece" as the current one and resets its position, then selects a new "next piece".
 */
game.createNewPiece = function () {
	// The new piece
	game.piece = game.nextPiece_;
	game.rotation = game.nextRotation_;
	game.posX = (this.board.BOARD_WIDTH / 2) + this.pieces.getXInitialPosition (game.piece, game.rotation);
	game.posY = game.pieces.getYInitialPosition (game.piece, game.rotation);

	// Random next piece
	game.nextPiece_ = this.getRand (0, 6);
	game.nextRotation_ = this.getRand (0, 3);
};

/*
 * DrawPiece is a really easy method that iterates through the piece matrix and draws each block of the piece.
 * It uses green for the normal blocks and blue for the pivot block.
 * For drawing the rectangles it calls to DrawRectangle method of the class "IO" that we will see later.
 */
game.drawPiece = function (x, y, piece, rotation) {
	var color;	 // Color of the block

	// Obtain the position in pixel in the screen of the block we want to draw
	var pixelsX = game.board.getXPosInPixels (x);
	var pixelsY = game.board.getYPosInPixels (y);

	// Travel the matrix of blocks of the piece and draw the blocks that are filled
	for (var i = 0; i < game.board.PIECE_BLOCKS; i++)
	{
		for (var j = 0; j < game.board.PIECE_BLOCKS; j++)
		{
			// Get the type of the block and draw it with the correct color
			switch (game.pieces.getBlockType (this.piece, this.rotation, j, i))
			{
				case 1: color = game.Color.GREEN; break;	// For each block of the piece except the pivot
				case 2: color = game.Color.BLUE; break;	// For the pivot
			}

			if (game.pieces.getBlockType (this.piece, this.rotation, j, i) != 0)
				game.io.drawRectangle(pixelsX + i * game.board.BLOCK_SIZE,
				pixelsY + j * game.board.BLOCK_SIZE,
				(pixelsX + i * game.board.BLOCK_SIZE) + game.board.BLOCK_SIZE - 1,
				(pixelsY + j * game.board.BLOCK_SIZE) + game.board.BLOCK_SIZE - 1,
				color);
		}
	}
};

/*
 * DrawBoard is similiar to the previous method. 
 * It draws two blue columns that are used as the limits of the boards.
 * Then draws the board blocks that are flagged as POS_FILLED in a nested loop.
 */
game.drawBoard = function () {
	var screenHeight = game.io.getScreenHeight();
	// Calculate the limits of the board in pixels
	var x1 = game.board.BOARD_POSITION - (game.board.BLOCK_SIZE * (game.board.BOARD_WIDTH / 2)) - 1;
	var x2 = game.board.BOARD_POSITION + (game.board.BLOCK_SIZE * (game.board.BOARD_WIDTH / 2));
	var y = screenHeight - (game.board.BLOCK_SIZE * game.board.BOARD_HEIGHT);

	// Check that the vertical margin is not to small
	//assert (mY > MIN_VERTICAL_MARGIN);

	// Rectangles that delimits the board
	game.io.drawRectangle(x1 - game.board.BOARD_LINE_WIDTH, y, x1, screenHeight - 1, game.Color.BLUE);

	game.io.drawRectangle(x2, y, x2 + game.board.BOARD_LINE_WIDTH, screenHeight - 1, game.Color.BLUE);

	// Check that the horizontal margin is not to small
	//assert (mX1 > MIN_HORIZONTAL_MARGIN);

	// Drawing the blocks that are already stored in the board
	x1 += 1;
	for (var i = 0; i < game.board.BOARD_WIDTH; i++) {
		for (var j = 0; j < game.board.BOARD_HEIGHT; j++) {
			// Check if the block is filled, if so, draw it
			if (game.board.isFreeBlock(i, j) == false)
				game.io.drawRectangle(x1 + i * game.board.BLOCK_SIZE,
	y + j * game.board.BLOCK_SIZE,
	(x1 + i * game.board.BLOCK_SIZE) + game.board.BLOCK_SIZE - 1,
	(y + j * game.board.BLOCK_SIZE) + game.board.BLOCK_SIZE - 1,
	game.Color.RED);
		}
	}
};

game.drawScene = function () {
	game.drawBoard ();	 // Draw the delimitation lines and blocks stored in the board
	game.drawPiece (this.posX, this.posY, this.piece, this.rotation);	 // Draw the playing piece
	game.drawPiece (this.nextPosX_, this.nextPosY_, this.nextPiece_, this.nextRotation_);	// Draw the next piece
};

//var Game = (function () {
//	var game = function () {
//		// public
//		this.posX = 0;
//		this.posY = 0;
//		this.piece = 0;
//		this.rotation = 0;

//		// private
//		var screenHeight;
//		var nextPosX, nextPosY;
//		var nextPiece, nextRotation;

//		this.getRand = function (a, b) {
//			return Math.random() * b + a;
//		};

//		this.initGame = function () {
//			// Init random numbers
//			//srand ((unsigned int) time(NULL));

//			// First piece
//			this.piece = this.getRand(0, 6);
//			this.rotation = this.getRand(0, 3);
//			posX = (game.board.BOARD_WIDTH / 2) + mPieces.GetXInitialPosition(mPiece, mRotation);
//			posY = mPieces.GetYInitialPosition(mPiece, mRotation);

//			// Next piece
//			mNextPiece = GetRand(0, 6);
//			mNextRotation = GetRand(0, 3);
//			mNextPosX = BOARD_WIDTH + 5;
//			mNextPosY = 5;
//		}
//	};

//	return game;
//})();

