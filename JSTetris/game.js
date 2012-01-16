goog.provide('game');
goog.require('game.pieces');
goog.require('game.board');

game.WAIT_TIME = 700; 	 // Number of milliseconds that the piece remains before going 1 block down */

/*
 * Get a random int between to integers
 */
game.getRand = function (a, b) {
	return Math.random() * b + a;
	//return Math.random() % (b - a + 1) + a;
}

game.initGame = function () {
// Init random numbers
//srand ((unsigned int) time(NULL));

// First piece
game.piece	 = this.getRand (0, 6);
mRotation	 = GetRand (0, 3);
mPosX = (BOARD_WIDTH / 2) + mPieces.GetXInitialPosition (mPiece, mRotation);
mPosY = mPieces.GetYInitialPosition (mPiece, mRotation);

// Next piece
mNextPiece = GetRand (0, 6);
mNextRotation = GetRand (0, 3);
mNextPosX = BOARD_WIDTH + 5;
mNextPosY = 5;
}