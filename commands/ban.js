const Discord = require('discord.js');

module.exports.run = async (bot, message, args) => {
    
    message.delete();

    if(!message.member.hasPermission("BAN_MEMBERS")) return message.channel.send("You don't have the correct permissions to ban people.").then(msg => msg.delete(7500));
    let banUser = message.guild.member(message.mentions.users.first() || message.guild.members.get(args[0]));
    if(!banUser) return message.channel.send("Can't find user!").then(msg => msg.delete(7500));
    let banReason = args.join(" ").slice(22);
    if(banUser.hasPermission("MANAGE_MESSAGES")) return message.channel.send("You can't ban that person.").then(msg => msg.delete(7500));

    let embed = new Discord.RichEmbed()
    .setColor("#c838f4")
    .addField("Banned", `${banUser}`)
    .addField("Reason", banReason)
    .addField("Channel", message.channel)
    .addField("By", `<@${message.author.id}>`)
    .addField("At", message.createdAt);
    
    let modLog = message.guild.channels.find("name", "modlog");
    if(!modLog) return message.channel.send("Can't find #modlog channel.").then(msg => msg.delete(7500));;

    message.guild.member(banUser).ban(banReason);
    modLog.send(embed);

    message.channel.send(`ADMIN-LOG: Successfully banned ${banUser.user.username}#${banUser.user.discriminator} from the server.`);
}

module.exports.help = {
    name: "ban"
}