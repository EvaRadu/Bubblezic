const listBalles = require('../objects/balls');

module.exports = function calculePos(posX, posY) {

    // Taille des deux écrans : 
    // Grand écran (player)
    x1 = -8.88;
    x2 = 8.8;
    y1 = -5;
    y2 = 5;

    // Petit écran (opponent)
    x3 = 5.8;
    x4 = 8.6;
    y3 = -4.6;
    y4 = -3.27;

    // Calcul de la position de la balle sur le petit écran
    var posXOpponent = ((posX - x1) / (x2 - x1)) * (x4-x3) + x3;
    var posYOpponent = ((posY - y1) / (y2 - y1)) * (y4-y3) + y3;

    return [posXOpponent, posYOpponent];
    
}