const Discord = require('discord.js');

module.exports.run = async (bot, message, args) => {

    message.delete();

    let embed = new Discord.RichEmbed()
        .setColor("#f73ba2")
        .setThumbnail(message.guild.iconURL)
        .addField("Name", message.guild.name)
        .addField("Owner", message.guild.owner.user.username)
        .addField("Member Count", `There are ${message.guild.memberCount} members in this discord.`)
        .addField("Created", message.guild.createdAt);

    message.channel.send(embed).then(msg => {msg.delete(7500)});
}

module.exports.help = {
    name: "serverinfo"
}