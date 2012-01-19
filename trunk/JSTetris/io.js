goog.provide('game.io');
goog.require('goog.graphics');

window.onload = function () {
	game.io.graphics = goog.graphics.createSimpleGraphics(window.innerWidth, window.innerHeight);
	game.io.graphics.render(document.getElementById('shapes'));
};

game.io.getScreenHeight = function () {
	return window.innerHeight;
};

game.io.isKeyDown = function (key) {
	var activeKeys = KeyboardJS.activeKeys();

	return (activeKeys.indexOf(key) != -1);
};

document.addEventListener('keydown', function() {
	//console.log(KeyboardJS.activeKeys());
	console.log(!game.io.isKeyDown("esc"));
});

game.io.drawRectangle = function (x1, y1, x2, y2, color) {
	var fill = new goog.graphics.SolidFill('yellow');
	var stroke = new goog.graphics.Stroke(2, 'green');

	this.graphics.drawRect(x1, y1, x2 - x1, y2 - y1, stroke, fill);
};

game.io.clearScreen = function () {
	this.graphics.clear();
};

game.io.updateScreen = function () {
	
};