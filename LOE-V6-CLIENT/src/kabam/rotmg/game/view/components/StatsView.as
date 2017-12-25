package kabam.rotmg.game.view.components {
import com.company.assembleegameclient.objects.Player;

import flash.display.Sprite;
import flash.events.MouseEvent;
import flash.filters.GlowFilter;

import kabam.rotmg.game.model.StatModel;
import kabam.rotmg.text.model.TextKey;

import org.osflash.signals.natives.NativeSignal;

public class StatsView extends Sprite {

    private static const statsModel:Array = [new StatModel(TextKey.STAT_MODEL_ATTACK_SHORT, TextKey.STAT_MODEL_ATTACK_LONG, TextKey.STAT_MODEL_ATTACK_DESCRIPTION, true), new StatModel(TextKey.STAT_MODEL_DEFENSE_SHORT, TextKey.STAT_MODEL_DEFENSE_LONG, TextKey.STAT_MODEL_DEFENSE_DESCRIPTION, false), new StatModel(TextKey.STAT_MODEL_SPEED_SHORT, TextKey.STAT_MODEL_SPEED_LONG, TextKey.STAT_MODEL_SPEED_DESCRIPTION, true), new StatModel(TextKey.STAT_MODEL_DEXTERITY_SHORT, TextKey.STAT_MODEL_DEXTERITY_LONG, TextKey.STAT_MODEL_DEXTERITY_DESCRIPTION, true), new StatModel(TextKey.STAT_MODEL_VITALITY_SHORT, TextKey.STAT_MODEL_VITALITY_LONG, TextKey.STAT_MODEL_VITALITY_DESCRIPTION, true), new StatModel(TextKey.STAT_MODEL_WISDOM_SHORT, TextKey.STAT_MODEL_WISDOM_LONG, TextKey.STAT_MODEL_WISDOM_DESCRIPTION, true)];
    public static const ATTACK:int = 0;
    public static const DEFENSE:int = 1;
    public static const SPEED:int = 2;
    public static const DEXTERITY:int = 3;
    public static const VITALITY:int = 4;
    public static const WISDOM:int = 5;
    public static const STATE_UNDOCKED:String = "state_undocked";
    public static const STATE_DOCKED:String = "state_docked";
    public static const STATE_DEFAULT:String = STATE_DOCKED;//"state_docked"

    private const WIDTH:int = 191;
    private const HEIGHT:int = 45;

    protected var _strAttack:Number;
    protected var _strDefense:Number;
    protected var _strSpeed:Number;
    protected var _strDexterity:Number;
    protected var _strVitality:Number;
    protected var _strWisdom:Number;

    private var background:Sprite;
    public var stats_:Vector.<StatView>;
    public var containerSprite:Sprite;
    public var mouseDown:NativeSignal;
    public var currentState:String = "state_docked";

    public function StatsView() {
        this.background = this.createBackground();
        this.stats_ = new Vector.<StatView>();
        this.containerSprite = new Sprite();
        super();
        addChild(this.background);
        addChild(this.containerSprite);
        this.createStats();
        mouseChildren = false;
        this.mouseDown = new NativeSignal(this, MouseEvent.MOUSE_DOWN, MouseEvent);
    }

    private function getStrAttack(player:Player):Number { return this._strAttack = Player.parse(player.attack_);}
    private function getStrDefense(player:Player):Number { return this._strDefense = Player.parse(player.defense_);}
    private function getStrSpeed(player:Player):Number { return this._strSpeed = Player.parse(player.speed_);}
    private function getStrDexterity(player:Player):Number { return this._strDexterity = Player.parse(player.dexterity_);}
    private function getStrVitality(player:Player):Number { return this._strVitality= Player.parse(player.vitality_);}
    private function getStrWisdom(player:Player):Number { return this._strWisdom = Player.parse(player.wisdom_);}

    private function createStats():void {
        var _local3:StatView;
        var _local1:int;
        var _local2:int;
        while (_local2 < statsModel.length)
        {
            _local3 = this.createStat(_local2, _local1);
            this.stats_.push(_local3);
            this.containerSprite.addChild(_local3);
            _local1 = (_local1 + (_local2 % 2));
            _local2++;
        }
    }

    private function createStat(_arg1:int, _arg2:int):StatView {
        var _local4:StatView;
        var _local3:StatModel = statsModel[_arg1];
        _local4 = new StatView(_local3.name, _local3.abbreviation, _local3.description, _local3.redOnZero);
        _local4.x = (((_arg1 % 2) * this.WIDTH) / 2);
        _local4.y = (_arg2 * (this.HEIGHT / 3));
        return (_local4);
    }

    public function draw(_arg1:Player, _arg2:Boolean = true, _arg3:Boolean = false):void {
        if (_arg1)
        {
            this.setBackgroundVisibility();
            this.drawStats(_arg1, _arg3);
        }
        if (_arg2)
            this.containerSprite.x = ((this.WIDTH - this.containerSprite.width) / 2);
    }

    private function drawStats(player:Player, isCharacterScreenFormat:Boolean):void {
        if (isCharacterScreenFormat) {
            this.stats_[ATTACK].draw(this.getStrAttack(player), player.attackBoost_, player.attackMax_, player.level_);
            this.stats_[DEFENSE].draw(this.getStrDefense(player), player.defenseBoost_, player.defenseMax_, player.level_);
            this.stats_[SPEED].draw(this.getStrSpeed(player), player.speedBoost_, player.speedMax_, player.level_);
            this.stats_[DEXTERITY].draw(this.getStrWisdom(player), player.dexterityBoost_, player.dexterityMax_, player.level_);
            this.stats_[VITALITY].draw(this.getStrDexterity(player), player.vitalityBoost_, player.vitalityMax_, player.level_);
            this.stats_[WISDOM].draw(this.getStrVitality(player), player.wisdomBoost_, player.wisdomMax_, player.level_);
        } else {
            this.stats_[ATTACK].draw(this.getStrAttack(player), player.attackBoost_, player.attackMax_, player.level_);
            this.stats_[DEFENSE].draw(this.getStrDefense(player), player.defenseBoost_, player.defenseMax_, player.level_);
            this.stats_[SPEED].draw(this.getStrSpeed(player), player.speedBoost_, player.speedMax_, player.level_);
            this.stats_[DEXTERITY].draw(this.getStrDexterity(player), player.dexterityBoost_, player.dexterityMax_, player.level_);
            this.stats_[VITALITY].draw(this.getStrVitality(player), player.vitalityBoost_, player.vitalityMax_, player.level_);
            this.stats_[WISDOM].draw(this.getStrWisdom(player), player.wisdomBoost_, player.wisdomMax_, player.level_);
        }
    }

    public function dock():void {
        this.currentState = STATE_DOCKED;
    }

    public function undock():void {
        this.currentState = STATE_UNDOCKED;
    }

    private function createBackground():Sprite {
        this.background = new Sprite();
        this.background.graphics.clear();
        this.background.graphics.beginFill(0x363636);
        this.background.graphics.lineStyle(2, 0xFFFFFF);
        this.background.graphics.drawRoundRect(-5, -5, (this.WIDTH + 10), (this.HEIGHT + 13), 10);
        this.background.filters = [new GlowFilter(0, 1, 10, 10, 1, 3)];
        return (this.background);
    }

    private function setBackgroundVisibility():void {
        if (this.currentState == STATE_UNDOCKED) {
            this.background.alpha = 1;
        }
        else {
            if (this.currentState == STATE_DOCKED) {
                this.background.alpha = 0;
            }
        }
    }


}
}
