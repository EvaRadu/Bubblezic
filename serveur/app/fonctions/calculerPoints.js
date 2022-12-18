const listBalles = require('../objects/balls');

module.exports = function calculePoints(time, id, type) {
    let ballTime = 0;

    listBalles.forEach(ball => {
        if (ball.id == id) {
            ballTime = ball.temps;
        }
    });

    console.log("time = " +time);

    if(type == 0){   // TYPE 0 = BALLES NORMALES
    
    if (time >= ballTime - 1 && time <= ballTime + 1) {
        return 5;
    } else if (time >= ballTime - 0.4 && time <= ballTime + 0.4) {
        return 6;
    } else if (time >= ballTime - 0.3 && time <= ballTime + 0.3) {
        return 7;
    } else if (time >= ballTime - 0.2 && time <= ballTime + 0.2) {
        return 8;
    } else if (time >= ballTime - 0.1 && time <= ballTime + 0.1) {
        return 9;
    } else if (time >= ballTime - 0.05 && time <= ballTime + 0.05) {
        return 10;
    } else {
        return 0;
    }
    }

    else if(type == 7){  // TYPE 7 = PUZZLE 
        return 5;
    }
}