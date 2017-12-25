package kabam.rotmg.chat.control {
import com.company.assembleegameclient.parameters.Parameters;

import kabam.rotmg.account.core.Account;
import kabam.rotmg.appengine.api.AppEngineClient;
import kabam.rotmg.build.api.BuildData;
import kabam.rotmg.chat.model.ChatMessage;
import kabam.rotmg.dailyLogin.model.DailyLoginModel;
import kabam.rotmg.game.signals.AddTextLineSignal;
import kabam.rotmg.text.model.TextKey;
import kabam.rotmg.ui.model.HUDModel;

public class ParseChatMessageCommand {

    [Inject]
    public var data:String;
    [Inject]
    public var hudModel:HUDModel;
    [Inject]
    public var addTextLine:AddTextLineSignal;
    [Inject]
    public var client:AppEngineClient;
    [Inject]
    public var account:Account;
    [Inject]
    public var buildData:BuildData;
    [Inject]
    public var dailyLoginModel:DailyLoginModel;


    public function execute():void {
        if(this.data.charAt(0) == "/") {
            var command:Array = this.data.substr(1, this.data.length).split(" ");
            switch (command[0]) {
                case "help":
                    this.addTextLine.dispatch(ChatMessage.make(Parameters.HELP_CHAT_NAME, TextKey.HELP_COMMAND));
                    return;
                case "mscale":
                    if(command.length > 1) {
                        var mscale:Number = Number(command[1]);
                        if(mscale >= 1 && mscale <= 5) {
                            var newMscale:Number = mscale*10;
                            Parameters.data_.mscale = newMscale;
                            Parameters.save();
                            this.addTextLine.dispatch(ChatMessage.make(Parameters.HELP_CHAT_NAME, "Map scale: " + mscale));
                        } else
                            this.addTextLine.dispatch(ChatMessage.make(Parameters.SERVER_CHAT_NAME, "Map scale only accept values between 1.0 to 5.0."));
                    } else
                        this.addTextLine.dispatch(ChatMessage.make(Parameters.HELP_CHAT_NAME, "Map scale: " + Parameters.data_.mscale / 10));
                    return;
            }
        }
        this.hudModel.gameSprite.gsc_.playerText(this.data);
    }


}
}
