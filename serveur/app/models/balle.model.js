const Joi  = require('joi');

/**
 * The Balle model
 */
module.exports = Joi.object().keys({
    Id: Joi.number().required,    // id de la balle
    posX: Joi.number().required,  // position X de la balle 
    posY: Joi.number().required,  // position Y de la balle
    couleur: Joi.string(),        // couleur de la balle
    rayon: Joi.number(),          // rayon de la balle
    temps: Joi.number(),          // a quel moment la balle apparait
    type: Joi.number(),           // différent type de balles, voir, l'objet type
    duration: Joi.number(),       // durée de vie de la balle
    rotation: Joi.number()        // rotation de la balle --> pour les demi cercles
})