!function(n) {
    const o = n.oc = n.oc || {};
    o.dictionary =
        Object.assign(o.dictionary || {},
            {
                "%0 of %1": "",
                Bold: "Gras",
                Cancel: "Anullar",
                "Cannot upload file:": "",
                Italic: "Italica",
                Save: "Enregistrar",
                "Show more items": ""
            }), o.getPluralForm = function(n) { return n > 1 }
}(window.CKEDITOR_TRANSLATIONS || (window.CKEDITOR_TRANSLATIONS = {}));