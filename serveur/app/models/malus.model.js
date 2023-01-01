const Joi  = require('joi');

/**
 * The Malus model
 */
module.exports = Joi.object().keys({
    Id: Joi.number().required,
    typeName : Joi.string().required,
    posX: Joi.number().required,
    posY: Joi.number().required,
    posXOpponent : Joi.number().required,
    posYOpponent : Joi.number().required,
    couleur: Joi.string(),
    rayon: Joi.number(),
    impulsion: Joi.number(),
    temps: Joi.number(),
    duration: Joi.number(),
    texture: Joi.string(),   
})

    