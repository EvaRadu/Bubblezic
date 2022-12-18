/*
module.exports = listBalles = [{
        id:1,
        posX: 1,
        posY: 1,
        couleur: 'red' ,
        rayon: 3,
        temps: 1,
        type: 0,
        duration: 10
    },{
        id: 2,
        posX: -3,
        posY: -3,
        couleur: 'blue' ,
        rayon: 2,
        temps: 3,
        type: 1,
        duration: 10
    },{
        id:3,
        posX: 3,
        posY: -4,
        couleur: 'blue' ,
        rayon: 1.5,
        temps: 1,
        type: 1,
        duration: 10
    },{
        id:4,
        posX: -4,
        posY: 2,
        couleur: 'red' ,
        rayon: 3,
        temps: 5,
        type: 0,
        duration: 10
    },{
        id:5,
        posX: 4,
        posY: 4,
        couleur: 'red' ,
        rayon: 2,
        temps: 7,
        type: 0,
        duration: 10
    },
    {
        id:4,
        posX: -4,
        posY: -2,
        couleur: 'blue' ,
        rayon: 3,
        temps: 1,
        type: 1,
        duration: 10
    },{
        id:5,
        posX: 2,
        posY: 1,
        couleur: 'blue' ,
        rayon: 2,
        temps: 1,
        type: 1,
        duration: 10
    }
];
*/

module.exports = listBalles = [

    { 
        id:1,
        typeName : "bubble",
        posX: 6,
        posY: 0,
        idTrajectoire: 1,
        couleur: 'yellow',
        rayon: 1,
        temps: 0,
        type: 1,
        duration: 15 
    },
    { 
        id:1,
        idBubble : 1,
        typeName : "trajectory",
        posX: 6,
        posY: 0,
        couleur: 'blue',
        temps: 0,
        duration: 15, 
        width : 4,
        height : 2,
    },

    { 
        id:2,
        typeName : "bubble",
        posX: 0,
        posY: 0,
        couleur: 'blue',
        rayon: 3,
        temps: 0,
        type: 6,
        duration: 15
    },

    { 
        id:3,
        typeName : "bubble",
        posX: -6,
        posY: 0,
        couleur: 'white',
        rayon: 3,
        temps: 0,
        type: 1,
        duration: 15
    },


];