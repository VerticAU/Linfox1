var Opportunity = Opportunity || {};
Opportunity.Form = (function () {
    var qualifiedContractOnChange = function () {
        var orderValue = Xrm.Page.getAttribute("vertic_qualifiedcontract").getValue();
        if (!orderValue) return;
        var windowOptions = {
            openInNewWindow: true
        };
        Xrm.Utility.openEntityForm("salesorder", orderValue[0].id, null, windowOptions)
    };
    var setHandlers = function () {
        Xrm.Page.getAttribute("vertic_qualifiedcontract").addOnChange(qualifiedContractOnChange);
    };
    var onLoad = function () {
        setHandlers();
    };
    return {
        OnLoad: onLoad
    };
})();