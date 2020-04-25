$(document).ready(function () {

    var checkBoxes = $('input:checkbox.checkbox');
    var checkedBoxeValue = function () {
        if (checkBoxes.is(':checked') === true) {
            return true;
        } else if (checkBoxes.is(':checked') === false) {
            return false;
        }
    }

    checkBoxes.change(function () {
        if ($(this).is(':checked')) {
            checkBoxes.prop('disabled', true);
            $(this).prop('disabled', false);
            return true;
        } else {
            checkBoxes.prop('disabled', false);
            return false;
        }
    });
    $('#submit').click(function () {
        var firstName = $('#firstName').val();
        var lastName = $('#lastName').val();
        var userName = $('#userName').val();
        var email = $('#email').val();
        var password = $('#password').val();
        if (firstName === "") {
            $('#firstNameError').html('First name must be required.');
        }
        if (lastName === "") {
            $('#lastNameError').html('Last name must be required.');
        }
        if (userName === "") {
            $('#userNameError').html('User name must be required.');
        }
        if (email === "") {
            $('#emailError').html('Email must be required.');
        }
        if (password === "") {
            $('#passwordError').html('Password must be required.');
        }
        var check = checkedBoxeValue();

        if (check === true) {
            $('#checkedErrorCheckBox').html("");
            return true;
        } else {
            $('#checkedErrorCheckBox').html("Please select at least one category of checkboxes");
            return false;
        }
    });
});