const Discord = require('discord.js');

module.exports.run = async (bot, message, args) => {
    
    message.delete();

    if (!message.member.hasPermission("KICK_MEMBERS")) return message.channel.send("You do not have the correct permissions to kick people.").then(msg => {msg.delete(7500)});
    
    let kickUser = message.guild.member(message.mentions.users.first() || message.guild.members.get(args[0]));
    if (kickUser.hasPermission("MANAGE_MESSAGES")) return message.channel.send("You do not have permission to ban this user.");
    if (!kickUser) return message.channel.send("Please provide a valid user mention.");

    let kickReason = args.join(" ").slice(22);
    if (!kickReason) return message.channel.send("Please provide a valid reason for the kick.");

    let modLog = message.guild.channels.find("name", "modlog");
    if (!modLog) return message.channel.send("Please create a #modlog channel.");

    let embed = new Discord.RichEmbed()
        .setColor("#c242f4")
        .addField("Kicked", kickUser.user.username)
        .addField("Reason", kickReason)
        .addField("Kicked By", message.author.username)
        .addField("Time", message.createdAt);

    message.guild.member(kickUser).kick(kickReason);
    modLog.send(embed);

    message.channel.send(`Successfully kicked ${kickUser.user.username} for ${kickReason}.`).then(msg => msg.delete(7500));

}

module.exports.help = {
    name: "kick"
}