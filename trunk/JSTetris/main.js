goog.provide('game.main');
goog.require('game');

/*
==================
Main
==================
*/
game.main.run = function () {

	// Class for drawing staff, it uses SDL for the rendering. Change the methods of this class
	// in order to use a different renderer
	var screenHeight = game.io.getScreenHeight();

	// Board
	game.board.initBoard();

	// Game
	game.initGame();

	// Get the actual clock milliseconds
	var time1 = new Date().getTime();

	//This is the main loop. We can exit by pressing ESC. In each frame we clear and update the screen and draw everything.

	while (game.io.isKeyDown("esc") == false) {
		// ----- Draw -----
		game.io.clearScreen(); // Clear screen
		game.drawScene();	 // Draw staff
		game.io.updateScreen ();	 // Put the graphic context in the screen
	}
};



//This is the main loop. We can exit by pressing ESC. In each frame we clear and update the screen and draw everything.

//[code language="c++"]
//// ----- Main Loop -----

//while (!mIO.IsKeyDown (SDLK_ESCAPE))
//{
//// ----- Draw -----

//mIO.ClearScreen (); // Clear screen
//mGame.DrawScene ();	 // Draw staff
//mIO.UpdateScreen ();	 // Put the graphic context in the screen
//[/code]

//We start with the input. If we press left, down or right we try to move the piece in that directions. We only move the piece if the movement is possible.

//[code language="c++"]
//// ----- Input -----

//int mKey = mIO.Pollkey();

//switch (mKey)
//{
//case (SDLK_RIGHT):
//{
//if (mBoard.IsPossibleMovement (mGame.mPosX + 1, mGame.mPosY, mGame.mPiece, mGame.mRotation))
//mGame.mPosX++;
//break;
//}

//case (SDLK_LEFT):
//{
//if (mBoard.IsPossibleMovement (mGame.mPosX - 1, mGame.mPosY, mGame.mPiece, mGame.mRotation))
//mGame.mPosX--;
//break;
//}

//case (SDLK_DOWN):
//{
//if (mBoard.IsPossibleMovement (mGame.mPosX, mGame.mPosY + 1, mGame.mPiece, mGame.mRotation))
//mGame.mPosY++;
//break;
//}
//[/code]

//By pressing "x", the piece will fall down directly to the ground. This is really easy to implement by trying to move the piece down until the movement is not possible. Then we store the piece, delete possible lines and check out if the game is over, if not, we create a new piece.

//[code language="c++"]
//case (SDLK_x):
//{
//// Check collision from up to down
//while (mBoard.IsPossibleMovement(mGame.mPosX, mGame.mPosY, mGame.mPiece, mGame.mRotation)) { mGame.mPosY++; }

//mBoard.StorePiece (mGame.mPosX, mGame.mPosY - 1, mGame.mPiece, mGame.mRotation);

//mBoard.DeletePossibleLines ();

//if (mBoard.IsGameOver())
//{
//mIO.Getkey();
//exit(0);
//}

//mGame.CreateNewPiece();

//break;
//}
//[/code]

//By pressing "z" we rotate the piece. With the methods that we have already implement this is an easy task. The rotation is in fact to change to the next rotated stored piece. We first should check that the rotated piece will be drawn without colliding, if so, we sets this rotation as the current one.

//[code language="c++"]
//case (SDLK_z):
//{
//if (mBoard.IsPossibleMovement (mGame.mPosX, mGame.mPosY, mGame.mPiece, (mGame.mRotation + 1) % 4))
//mGame.mRotation = (mGame.mRotation + 1) % 4;

//break;
//}
//}
//[/code]

//If WAIT_TIME passed, the piece should fall down one block. We have to check out if the movement is possible, if not, the piece should be stored and we have to check if we can delete lines. We also see if the game is over, if not, we create a new piece.

//[code language="c++"]
//// ----- Vertical movement -----

//unsigned long mTime2 = SDL_GetTicks();

//if ((mTime2 - mTime1) > WAIT_TIME)
//{
//if (mBoard.IsPossibleMovement (mGame.mPosX, mGame.mPosY + 1, mGame.mPiece, mGame.mRotation))
//{
//mGame.mPosY++;
//}
//else
//{
//mBoard.StorePiece (mGame.mPosX, mGame.mPosY, mGame.mPiece, mGame.mRotation);

//mBoard.DeletePossibleLines ();

//if (mBoard.IsGameOver())
//{
//mIO.Getkey();
//exit(0);
//}

//mGame.CreateNewPiece();
//}

//mTime1 = SDL_GetTicks();
//}
//}

//return 0;
//}