const Discord = require('discord.js');

module.exports.run = async (bot, message, args) => {
    message.delete();

    let embed = new Discord.RichEmbed()
        .setColor("#f4429e")
        .setTimestamp(message.createdAt)
        .setThumbnail(bot.user.avatarURL)
        .addField("Name", bot.user.username)
        .addField("Tag", bot.user.discriminator)
        .addField("Guilds", `Running on ${bot.guilds.size} servers with ${bot.users.size} users total.`);

    message.channel.send(embed).then(msg => msg.delete(7500));
}

module.exports.help = {
    name: "botinfo"
}