const Discord = require('discord.js');

module.exports.run = async (bot, message, args) => {
    
    message.delete();

    if(!message.member.hasPermission("KICK_MEMBERS")) return message.channel.send("You don't have the correct permissions to kick people.").then(msg => msg.delete(7500));
    let kickUser = message.guild.member(message.mentions.users.first() || message.guild.members.get(args[0]));
    if(!kickUser) return message.channel.send("Can't find user!").then(msg => msg.delete(7500));
    let kickReason = args.join(" ").slice(22);
    if(kickUser.hasPermission("MANAGE_MESSAGES")) return message.channel.send("You can't kick that person.").then(msg => msg.delete(7500));

    let embed = new Discord.RichEmbed()
    .setColor("#c838f4")
    .addField("Kicked", `${kickUser}`)
    .addField("Reason", kickReason)
    .addField("Channel", message.channel)
    .addField("By", `<@${message.author.id}>`)
    .addField("Time", message.createdAt);
    
    let modLog = message.guild.channels.find("name", "modlog");
    if(!modLog) return message.channel.send("Can't find #modlog channel.").then(msg => msg.delete(7500));;

    message.guild.member(kickUser).kick(kickReason);
    modLog.send(embed);

    message.channel.send(`ADMIN-LOG: Successfully kicked ${kUser.user.username}#${kUser.user.discriminator} from the server.`);


}

module.exports.help = {
    name: "kick"
}