const Discord = require('discord.js');

module.exports.run = async (bot, message, args) => {

    message.channel.send("I stream every Sunday and Wednesday.").then(msg => msg.delete(7500));

}

module.exports.help = {
    name: "schedule"
}