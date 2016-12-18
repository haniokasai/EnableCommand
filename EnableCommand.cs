using log4net;
using MiNET;
using MiNET.Net;
using MiNET.Plugins;
using MiNET.Plugins.Attributes;
using MiNET.Plugins.Commands;

namespace EnableCommand
{
    [Plugin(PluginName = "EnableCommand", Description = "You can enable to use command!", PluginVersion = "1.0", Author = "haniokasai")]
    public class EnableCommand : Plugin
    {
        protected static ILog _log = LogManager.GetLogger("EnableCommand");


        protected override void OnEnable()
        {
            Context.PluginManager.LoadCommands(new HelpCommand(Context.Server.PluginManager));// /helpを使えるようにする
            Context.PluginManager.LoadCommands(new VanillaCommands(Context.Server.PluginManager));// /opを使えるようにする
            Context.Server.PlayerFactory.PlayerCreated += PlayerFactory_PlayerCreated;
            _log.Warn("Loaded");
        }

        private void PlayerFactory_PlayerCreated(object sender, PlayerEventArgs e)
        {
            var player = e.Player;
            player.PlayerJoin += Player_PlayerJoin;//generate player join event
        }

        private void Player_PlayerJoin(object sender, PlayerEventArgs e)
        {
            Player player = e.Player;
            var setCmdEnabled = McpeSetCommandsEnabled.CreateObject();
            setCmdEnabled.enabled = true;
            player.SendPackage(setCmdEnabled);
        }

    }
}
