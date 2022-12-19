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
        couleur: 'magenta',
        rayon: 1,
        temps: 0,
        type: 1,
        duration: 10,
        rotation: 90,
        side: 0,
        texture: ""
    },
    { 
        id:1,
        idBubble : 1,
        typeName : "trajectory",
        posX: 6,
        posY: 0,
        couleur: 'blue',
        temps: 0,
        duration: 10, 
        width : 4,
        height : 2,
    },
    { 
        id:2,
        typeName : "bubble",
        posX: 6,
        posY: 2,
        couleur: 'white',
        rayon: 3,
        temps: 10,
        type: 0,
        duration: 3,
        rotation: 90,
        side: 0,
        texture: ""
    },
    { 
        id:3,
        typeName : "bubble",
        posX: -6,
        posY: -1,
        couleur: 'white',
        rayon: 3,
        temps: 10,
        type: 0,
        duration: 3,
        rotation: 90,
        side: 0,
        texture: ""
    },
    { 
        id:6,
        typeName : "bubble",
        posX: -6,
        posY: 0,
        couleur: 'white',
        rayon: 3,
        temps: 13,
        type: 7,
        duration: 3,
        rotation: 90,
        side: 1,
        texture: ""
        },
        { 
        id:7,
        typeName : "bubble",
        posX: 6,
        posY: 0,
        couleur: 'white',
        rayon: 3,
        temps: 13,
        type: 7,
        duration: 3,
        rotation: -90,
        side: 2,
        texture: ""
    },
    { 
        id:8,
        typeName : "bubble",
        posX: 0,
        posY: 0,
        couleur: 'magenta',
        rayon: 3,
        temps: 13,
        type: 6,
        duration: 3,
        rotation: 90,
        side: 0,
        texture: "ciblePuzzle"
    },

    { 
        id:9,
        typeName : "bubble",
        posX: 0,
        posY: 2,
        couleur: 'white',
        rayon: 3,
        temps: 16,
        type: 0,
        duration: 3,
        rotation: 90,
        side: 0,
        texture: ""
    },
    { 
        id:10,
        typeName : "bubble",
        posX: 3,
        posY: 3,
        couleur: 'yellow',
        rayon: 2,
        temps: 19,
        type: 0,
        duration: 3,
        rotation: 90,
        side: 0,
        texture: ""
    },
    { 
        id:11,
        typeName : "bubble",
        posX: 3,
        posY: -3,
        couleur: 'cyan',
        rayon: 2,
        temps: 19,
        type: 0,
        duration: 3,
        rotation: 90,
        side: 0,
        texture: ""
    },
    { 
        id:12,
        typeName : "bubble",
        posX: -3,
        posY: 3,
        couleur: 'yellow',
        rayon: 2,
        temps: 19,
        type: 0,
        duration: 2,
        rotation: 90,
        side: 0,
        texture: ""
    },
    { 
        id:13,
        typeName : "bubble",
        posX: -3,
        posY: -3,
        couleur: 'cyan',
        rayon: 2,
        temps: 19,
        type: 0,
        duration: 2,
        rotation: 90,
        side: 0,
        texture: ""
    }
];