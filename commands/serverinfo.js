const Discord = require('discord.js');

module.exports.run = async (bot, message, args) => {

    let embed = new Discord.RichEmbed()
        .setColor()
        .addField("Name", message.guild.name)
        .addField("Owner", message.guild.owner.user.username);

    message.channel.send(embed).then(msg => {msg.delete(7500)});
}

module.exports.help = {
    name: "serverinfo"
}