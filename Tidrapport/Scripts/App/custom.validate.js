//$.validator.methods.number = function (value, element) {
//    return this.optional(element) || !isNaN(Globalize.parseFloat(value));
//}

//$.validator.methods.range = function (value, element, param) {
//    return this.optional(element) || (Globalize.parseFloat(value) >= param[0] && Globalize.parseFloat(value) <= param[1]);
//}

//$(document).ready(function () {
//    Globalize.culture('sv-SE');

//    // Only there to show which culture are being used.
//    console.log(Globalize.culture().name);
//});

$.validator.methods.range = function (value, element, param) {
    var globalizedValue = value.replace(",", ".");
    return this.optional(element) || (globalizedValue >= param[0] && globalizedValue <= param[1]);
}

$.validator.methods.number = function (value, element) {
    return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(value);
};
