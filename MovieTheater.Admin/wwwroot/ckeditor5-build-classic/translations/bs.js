!function(e) {
    const i = e.bs = e.bs || {};
    i.dictionary =
        Object.assign(i.dictionary || {},
            {
                "%0 of %1": "%0 od %1",
                "Block quote": "Citat",
                Bold: "Podebljano",
                "Break text": "",
                Cancel: "Poništi",
                "Cannot upload file:": "Nije moguće učitati fajl:",
                "Centered image": "Centrirana slika",
                "Change image text alternative": "Promijeni ALT atribut za sliku",
                "Choose heading": "Odaberi naslov",
                "Could not insert image at the current position.": "Nije moguće umetnuti sliku na poziciju.",
                "Could not obtain resized image URL.": "Nije moguće dobiti URL slike.",
                "Enter image caption": "Unesi naziv slike",
                "Full size image": "",
                Heading: "Naslov",
                "Heading 1": "Naslov 1",
                "Heading 2": "Naslov 2",
                "Heading 3": "Naslov 3",
                "Heading 4": "Naslov 4",
                "Heading 5": "Naslov 5",
                "Heading 6": "Naslov 6",
                "Image toolbar": "",
                "image widget": "",
                "In line": "",
                "Insert image": "Umetni sliku",
                "Insert image or file": "Umetni sliku ili datoteku",
                "Inserting image failed": "Umetanje slike neuspješno",
                Italic: "Zakrivljeno",
                "Left aligned image": "Lijevo poravnata slika",
                Paragraph: "Paragraf",
                "Right aligned image": "Desno poravnata slika",
                Save: "Sačuvaj",
                "Selecting resized image failed": "Odabir slike nije uspješan",
                "Show more items": "Prikaži više stavki",
                "Side image": "",
                "Text alternative": "ALT atribut",
                "Upload failed": "Učitavanje slike nije uspjelo",
                "Wrap text": "Prelomi tekst"
            }), i.getPluralForm = function(e) {
        return e % 10 == 1 && e % 100 != 11 ? 0 : e % 10 >= 2 && e % 10 <= 4 && (e % 100 < 10 || e % 100 >= 20) ? 1 : 2
    }
}(window.CKEDITOR_TRANSLATIONS || (window.CKEDITOR_TRANSLATIONS = {}));