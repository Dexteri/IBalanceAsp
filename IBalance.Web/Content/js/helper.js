function getId(text) {
    var tmp = text.indexOf('_');
    return text.substring(Number(tmp + 1));
};

function getCheckedBoxes(chkboxName) {
    var checkboxes = document.getElementsByName(chkboxName);
    var checkboxesChecked = [];
    // loop over them all
    for (var i = 0; i < checkboxes.length; i++) {
        // And stick the checked ones onto an array...
        if (checkboxes[i].checked) {
            checkboxesChecked.push(checkboxes[i]);
        }
    }
    // Return the array if it is non-empty, or null
    return checkboxesChecked.length > 0 ? checkboxesChecked : null;
}

function validatePhone(phone) {
    var phoneRegex = /^\+7\([0-9]{3}\)[0-9]{3}-[0-9]{2}-[0-9]{2}$/;
    return phoneRegex.test(phone);
}