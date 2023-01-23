module.exports = listBalles = [

    // BULLE SIMPLE 
   
    { 
        id:0,
        typeName : "bubble",
        posX: 0,
        posY: 0,
        couleur: 'red',
        rayon: 3,
        temps: 1,
        type: 0,
        duration: 2,
        rotation: 90,
        side: 0,
        texture: "bubble2"
    },

    // DEUX BULLES EN MEME TEMPS
    { 
        id:1,
        typeName : "bubble",
        posX: -2,
        posY: -2,
        couleur: 'magenta',
        rayon: 3,
        temps: 6,
        type: 0,
        duration: 2,
        rotation: 90,
        side: 0,
        texture: "bubble2"
    },

    { 
        id:2,
        typeName : "bubble",
        posX: 2,
        posY: 2,
        couleur: 'magenta',
        rayon: 3,
        temps: 6,
        type: 0,
        duration: 2,
        rotation: 90,
        side: 0,
        texture: "bubble2"
    },



    // TOUCHER PROLONGER
    { 
        id:3,
        typeName : "bubble",
        posX: 2,
        posY: 0,
        idTrajectoire: 1,
        couleur: 'magenta',
        rayon: 1,
        temps: 11,
        type: 1,
        duration: 3,
        rotation: 90,
        side: 0,
        texture: "bubble2"
    },
    { 
        id:5,
        typeName : "bubble",
        idTrajectoire: 1,
        posX: 5,
        posY: 0,
        couleur: 'grey',
        rayon: 1,
        temps: 11,
        type: 9,
        duration: 3,
        rotation: 90,
        side: 0,
        texture: "cibleToucherProlonger"
    },
    { 
        id:4,
        idBubble : 4,
        idCible : 5,
        typeName : "trajectory",
        posX: 4,
        posY: 0,
        couleur: 'blue',
        temps: 11,
        duration: 3, 
        width : 5,
        height : 3,
    },

    // PUZZLE
    {
        id: 6,
        typeName: "bubble",
        posX: -3,
        posY: 0,
        couleur: 'white',
        rayon: 3,
        temps: 17,
        type: 7,
        duration: 3,
        rotation: 90,
        side: 1,
        texture: ""
    },
    {
        id: 7,
        typeName: "bubble",
        posX: 3,
        posY: 0,
        couleur: 'white',
        rayon: 3,
        temps: 17,
        type: 7,
        duration: 3,
        rotation: -90,
        side: 2,
        texture: ""
    },
    {
        id: 8,
        typeName: "bubble",
        posX: 0,
        posY: 0,
        couleur: 'yellow',
        rayon: 3,
        temps: 17,
        type: 6,
        duration: 3,
        rotation: 90,
        side: 0,
        texture: "ciblePuzzle"
    },

    // MALUS 
    { 
        id:9,
        typeName : "bubble",
        type: 4,

        posX: 0,
        posY: 0,
        posXOpponent : 0,
        posYOpponent : 0,
        impulsion: 0.5,

        couleur: 'white',
        rayon: 3,
        temps: 23,
        duration: 3,
        rotation: 90,
        side: 0,
        freezeDuration : 5,
        texture: "flocon"
    },

    // BULLES SIMPLES
    {
        id: 10,
        typeName: "bubble",
        posX: -2,
        posY: 2,
        couleur: 'green',
        rayon: 3,
        temps: 26,
        type: 0,
        duration: 2,
        rotation: 90,
        side: 0,
        texture: "bubble2"
    },

    {
        id: 11,
        typeName: "bubble",
        posX: 2,
        posY: -2,
        couleur: 'green',
        rayon: 3,
        temps: 26,
        type: 0,
        duration: 2,
        rotation: 90,
        side: 0,
        texture: "bubble2"
    },

    // PUZZLE
    {
        id: 12,
        typeName: "bubble",
        posX: -3,
        posY: 0,
        couleur: 'white',
        rayon: 3,
        temps: 31,
        type: 7,
        duration: 3,
        rotation: 90,
        side: 1,
        texture: ""
    },
    {
        id: 13,
        typeName: "bubble",
        posX: 3,
        posY: 0,
        couleur: 'white',
        rayon: 3,
        temps: 31,
        type: 7,
        duration: 3,
        rotation: -90,
        side: 2,
        texture: ""
    },
    {
        id: 14,
        typeName: "bubble",
        posX: 0,
        posY: 0,
        couleur: 'magenta',
        rayon: 3,
        temps: 31,
        type: 6,
        duration: 3,
        rotation: 90,
        side: 0,
        texture: "ciblePuzzle"
    },

    // MALUS MULTIPLE
    { 
        id:15,
        typeName : "bubble",
        type: 5,
         posX: 0,
         posY: 0,
         posXOpponent : 0,
         posYOpponent : 0,
         impulsion: 0.5,

         couleur: 'green',
         rayon: 2,
         temps: 37,
         duration: 3,
         rotation: 90,
         side: 0,
         nbMalusMultiple : 2,
         texture: ""
     },



]