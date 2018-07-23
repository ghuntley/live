const Discord = require('discord.js');

module.exports.run = async (bot, message, args) => {

    message.channel.send(`Started: ${bot.readyAt}.`).then(msg => msg.delete(7500));    

}

module.exports.help = {
    name: "uptime"
}