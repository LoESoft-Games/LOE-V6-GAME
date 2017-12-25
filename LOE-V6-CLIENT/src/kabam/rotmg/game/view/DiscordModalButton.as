package kabam.rotmg.game.view {
import com.company.assembleegameclient.parameters.Parameters;
import com.company.assembleegameclient.sound.SoundEffectLibrary;
import com.company.assembleegameclient.util.TextureRedrawer;
import com.company.util.AssetLibrary;

import flash.display.Bitmap;
import flash.display.BitmapData;
import flash.display.Sprite;
import flash.events.MouseEvent;
import flash.filters.DropShadowFilter;
import flash.geom.Rectangle;
import flash.net.URLRequest;
import flash.net.navigateToURL;

import kabam.rotmg.text.view.TextFieldDisplayConcrete;
import kabam.rotmg.text.view.stringBuilder.StaticStringBuilder;
import kabam.rotmg.ui.UIUtils;

public class DiscordModalButton extends Sprite {
    public static const IMAGE_NAME:String = "discordIcon";
    public static const IMAGE_ID:int = 0;

    private var bitmap:Bitmap;
    private var background:Sprite;
    private var background2:Sprite;
    private var icon:BitmapData;
    private var text:TextFieldDisplayConcrete;

    public function DiscordModalButton() {
        mouseChildren = false;
        this.icon = TextureRedrawer.redraw(AssetLibrary.getImageFromSet(IMAGE_NAME, IMAGE_ID), 32, true, 0);
        this.bitmap = new Bitmap(this.icon);
        this.bitmap.x = 1;
        this.bitmap.y = 0;
        this.bitmap.width = this.bitmap.height = 28;
        this.background = UIUtils.makeStaticHUDBackground();
        this.background2 = UIUtils.makeHUDBackground(31, UIUtils.NOTIFICATION_BACKGROUND_HEIGHT);
        this.text = new TextFieldDisplayConcrete().setSize(16).setColor(0xFFFFFF);
        this.text.setStringBuilder(new StaticStringBuilder("Join Us!"));
        this.text.filters = [new DropShadowFilter(0, 0, 0, 1, 4, 4, 2)];
        this.text.setVerticalAlign(TextFieldDisplayConcrete.BOTTOM);
        this.drawAsOpen();
        var _local_1:Rectangle = this.bitmap.getBounds(this);
        var _local_2:int = 4;
        this.text.x = (_local_1.right - _local_2);
        this.text.y = (_local_1.bottom - _local_2*2 + 2);
        addEventListener(MouseEvent.CLICK, this.onClick);
    }

    public function onClick(_arg_1:MouseEvent):void {
        var _local_1:String = Parameters.DISCORD_PERMANENTLY_INVITE;
        var _local_2:URLRequest = new URLRequest();
        _local_2.url = _local_1;
        SoundEffectLibrary.play("button_click");
        navigateToURL(_local_2, "_blank");
    }

    public function drawAsOpen():void {
        addChild(this.background);
        addChild(this.text);
        addChild(this.bitmap);
    }
}
}