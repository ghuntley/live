const Discord = require('discord.js');

module.exports.run = async (bot, message, args) => {

    message.delete();

    let embed = new Discord.RichEmbed()
        .setColor("#f73ba2")
        .addField("Name", message.guild.name)
        .addField("Owner", message.guild.owner.user.username)
        .addField("Created", message.guild.createdAt)
        .addField("Joined", message.member.joinedAt)
        .addField("Bans", `There is ${message.guild.fetchBans.length} ban(s) on this discord.`)
        .addField("Member Count", `There are ${message.guild.memberCount} members in this discord.`)
        .addField("Region", `This server is hosted in ${message.guild.region.toUpperCase()}!`);

    message.channel.send(embed).then(msg => msg.delete(7500));
}

module.exports.help = {
    name: "serverinfo"
}