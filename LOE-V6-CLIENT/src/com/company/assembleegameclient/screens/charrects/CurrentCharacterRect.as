package com.company.assembleegameclient.screens.charrects {
import com.company.assembleegameclient.appengine.CharacterStats;
import com.company.assembleegameclient.appengine.SavedCharacter;
import com.company.assembleegameclient.screens.events.DeleteCharacterEvent;
import com.company.assembleegameclient.ui.tooltip.MyPlayerToolTip;
import com.company.assembleegameclient.util.FameUtil;
import com.company.rotmg.graphics.DeleteXGraphic;

import flash.display.Bitmap;

import flash.display.DisplayObject;
import flash.display.Sprite;
import flash.events.Event;
import flash.events.MouseEvent;

import kabam.rotmg.classes.model.CharacterClass;
import kabam.rotmg.text.model.TextKey;
import kabam.rotmg.text.view.TextFieldDisplayConcrete;
import kabam.rotmg.text.view.stringBuilder.LineBuilder;
import kabam.rotmg.text.view.stringBuilder.StaticStringBuilder;

import org.osflash.signals.Signal;

public class CurrentCharacterRect extends CharacterRect {

    private static var toolTip_:MyPlayerToolTip = null;

    public const selected:Signal = new Signal();
    public const deleteCharacter:Signal = new Signal();
    public const showToolTip:Signal = new Signal(Sprite);
    public const hideTooltip:Signal = new Signal();

    public var charName:String;
    public var charStats:CharacterStats;
    public var char:SavedCharacter;
    public var myPlayerToolTipFactory:MyPlayerToolTipFactory;
    private var charType:CharacterClass;
    private var deleteButton:Sprite;
    private var icon:DisplayObject;
    private var petIcon:Bitmap;
    protected var statsMaxedText:TextFieldDisplayConcrete;

    public function CurrentCharacterRect(_arg1:String, _arg2:CharacterClass, _arg3:SavedCharacter, _arg4:CharacterStats) {
        this.myPlayerToolTipFactory = new MyPlayerToolTipFactory();
        super();
        this.charName = _arg1;
        this.charType = _arg2;
        this.char = _arg3;
        this.charStats = _arg4;
        var _local5:String = _arg2.name;
        var _local6:int = _arg3.charXML_.Level;
        super.className = new LineBuilder().setParams(TextKey.CURRENT_CHARACTER_DESCRIPTION, {
            "className": _local5,
            "level": _local6
        });
        super.color = 0x5C5C5C;
        super.overColor = 0x7F7F7F;
        super.init();
        this.makeTagline();
        this.makeDeleteButton();
        this.makePetIcon();
        this.makeStatsMaxedText();
        this.addEventListeners();
    }

    private function addEventListeners():void {
        addEventListener(Event.REMOVED_FROM_STAGE, this.onRemovedFromStage);
        selectContainer.addEventListener(MouseEvent.CLICK, this.onSelect);
        this.deleteButton.addEventListener(MouseEvent.CLICK, this.onDelete);
    }

    private function onSelect(_arg1:MouseEvent):void {
        this.selected.dispatch(this.char);
    }

    private function onDelete(_arg1:MouseEvent):void {
        this.deleteCharacter.dispatch(this.char);
    }

    public function setIcon(_arg1:DisplayObject):void {
        ((this.icon) && (selectContainer.removeChild(this.icon)));
        this.icon = _arg1;
        this.icon.x = CharacterRectConstants.ICON_POS_X;
        this.icon.y = CharacterRectConstants.ICON_POS_Y;
        ((this.icon) && (selectContainer.addChild(this.icon)));
    }

    private function makePetIcon():void{
        var _local1:* = this.char.getPetVO();
        if (_local1)
        {
            this.petIcon = _local1.getSkin();
            if (this.petIcon != null)
            {
                this.petIcon.x = CharacterRectConstants.PET_ICON_POS_X;
                this.petIcon.y = CharacterRectConstants.PET_ICON_POS_Y;
                selectContainer.addChild(this.petIcon);
            }
        }
    }

    private function makeTagline():void {
        if (this.getNextStarFame() > 0) {
            super.makeTaglineIcon();
            super.makeTaglineText(new LineBuilder().setParams(TextKey.CURRENT_CHARACTER_TAGLINE, {
                "fame": this.char.fame(),
                "nextStarFame": this.getNextStarFame()
            }));
            taglineText.x = (taglineText.x + taglineIcon.width);
        }
        else {
            super.makeTaglineIcon();
            super.makeTaglineText(new LineBuilder().setParams(TextKey.CURRENT_CHARACTER_TAGLINE_NOQUEST, {"fame": this.char.fame()}));
            taglineText.x = (taglineText.x + taglineIcon.width);
        }
    }

    private function getNextStarFame():int {
        return (FameUtil.nextStarFame((((this.charStats == null)) ? 0 : this.charStats.bestFame()), this.char.fame()));
    }

    private function makeDeleteButton():void {
        this.deleteButton = new DeleteXGraphic();
        this.deleteButton.addEventListener(MouseEvent.MOUSE_DOWN, this.onDeleteDown);
        this.deleteButton.x = (WIDTH - 40);
        this.deleteButton.y = ((HEIGHT - this.deleteButton.height) * 0.5);
        addChild(this.deleteButton);
    }

    private function makeStatsMaxedText():void{
        var _local1:int = this.getMaxedStats();
        var _local2:uint = 0xB3B3B3;
        if (_local1 == 8)
            _local2 = 0xFCDF00;
        this.statsMaxedText = new TextFieldDisplayConcrete().setSize(18).setColor(0xFFFFFF);
        this.statsMaxedText.setBold(true);
        this.statsMaxedText.setColor(_local2);
        this.statsMaxedText.setStringBuilder(new StaticStringBuilder((_local1 + "/8")));
        this.statsMaxedText.filters = makeDropShadowFilter();
        this.statsMaxedText.x = CharacterRectConstants.STATS_MAXED_POS_X;
        this.statsMaxedText.y = CharacterRectConstants.STATS_MAXED_POS_Y;
        selectContainer.addChild(this.statsMaxedText);
    }

    private function getMaxedStats():int{
        var _local1:int;
        //Patch?
        var weirdMaxDexterity:int = this.charType.hpRegeneration.max;
        var weirdMaxVitality:int = this.charType.mpRegeneration.max;
        var weirdMaxWisdom:int = this.charType.dexterity.max;
        if (this.char.hp() == this.charType.hp.max)
            _local1++;
        if (this.char.mp() == this.charType.mp.max)
            _local1++;
        if (this.char.att() == this.charType.attack.max)
            _local1++;
        if (this.char.def() == this.charType.defense.max)
            _local1++;
        if (this.char.spd() == this.charType.speed.max)
            _local1++;
        if (this.char.dex() == weirdMaxDexterity)
            _local1++;
        if (this.char.vit() == weirdMaxVitality)
            _local1++;
        if (this.char.wis() == weirdMaxWisdom)
            _local1++;
        return (_local1);
    }

    override protected function onMouseOver(_arg1:MouseEvent):void {
        super.onMouseOver(_arg1);
        this.removeToolTip();
        toolTip_ = this.myPlayerToolTipFactory.create(this.charName, this.char.charXML_, this.charStats);
        toolTip_.createUI();
        this.showToolTip.dispatch(toolTip_);
    }

    override protected function onRollOut(_arg1:MouseEvent):void {
        super.onRollOut(_arg1);
        this.removeToolTip();
    }

    private function onRemovedFromStage(_arg1:Event):void {
        this.removeToolTip();
    }

    private function removeToolTip():void {
        this.hideTooltip.dispatch();
    }

    private function onDeleteDown(_arg1:MouseEvent):void {
        _arg1.stopImmediatePropagation();
        dispatchEvent(new DeleteCharacterEvent(this.char));
    }


}
}
