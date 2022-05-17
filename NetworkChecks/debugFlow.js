import { DropdownStepView, InfoStepView, StepFlow, StepFlowManager, CheckStepView, StepViewContainer, InputStepView, ButtonStepView, PromiseCompletionSource, TelemetryService } from 'diagnostic-data';
export var debugFlow = {
    title: "local debug flow",
    async func(siteInfo, diagProvider, flowMgr) {
        flowMgr.addView(new InfoStepView({
            infoType: 1,
            title: "You can edit this flow and test",
            markdown: "Modify this file and hit the refresh button, the change will take effect immediately" 
        }));
    }
}