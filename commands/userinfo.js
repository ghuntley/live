const Discord = require('discord.js');

module.exports.run = async (bot, message, args) => {

    message.delete();

    let user = message.guild.member(message.mentions.users.first() || message.guild.members.get(args[0]) || message.author);

    let embed = new Discord.RichEmbed()
        .setColor("#e8833c")
        .addField("Username", user.user.username)
        .addField("Tag", user.user.discriminator)
        .addField("Status", user.user.presence.status.toUpperCase())
        .addField("Joined", user.guild.joinedAt)
        .addField("Account Created", user.user.createdAt);

    message.channel.send(embed).then(msg => {msg.delete(7500)});
}

module.exports.help = {
    name: "userinfo"
}