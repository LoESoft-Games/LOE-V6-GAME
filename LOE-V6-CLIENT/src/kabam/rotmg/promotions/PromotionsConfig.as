package kabam.rotmg.promotions {
import kabam.rotmg.promotions.view.WebChoosePaymentTypeDialog;
import kabam.rotmg.promotions.view.WebChoosePaymentTypeDialogMediator;
import robotlegs.bender.extensions.mediatorMap.api.IMediatorMap;
import robotlegs.bender.framework.api.IConfig;

public class PromotionsConfig implements IConfig {
    [Inject]
    public var mediatorMap:IMediatorMap;

    public function configure():void {
        this.mediatorMap.map(WebChoosePaymentTypeDialog).toMediator(WebChoosePaymentTypeDialogMediator);
    }
}
}