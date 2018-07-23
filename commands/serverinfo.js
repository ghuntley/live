const Discord = require('discord.js');

module.exports.run = async (bot, message, args) => {

    message.delete();

    let embed = new Discord.RichEmbed()
        .setColor("#f73ba2")
        .setThumbnail(message.guild.iconURL)
        .addField("Name", message.guild.name)
        .addField("Owner", message.guild.owner.user.username)
        .addField("Created", message.guild.createdAt)
        .addField("Joined", message.member.joinedAt)
        .addField("Bans", `There are ${message.guild.fetchBans.length} user(s) banned in this discord.`)
        .addField("Member Count", `There are ${message.guild.memberCount} user(s) in this discord.`)
        .addField("Region", `This server is hosted in ${message.guild.region.toUpperCase()}.`);

    message.channel.send(embed).then(msg => msg.delete(7500));
}

module.exports.help = {
    name: "serverinfo"
}