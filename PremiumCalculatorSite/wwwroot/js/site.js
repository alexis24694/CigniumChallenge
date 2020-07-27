const url = new URL('https://localhost:44364/api/premiumrule/premiumvalue');

function setPremiumValue() {
    const txtDateOfBirth = document.getElementById('txtDateOfBirth');
    const txtState = document.getElementById('txtState');
    const txtAge = document.getElementById('txtAge');
    let txtPremiumValue = document.getElementById('txtPremiumValue');

    const dateOfBirth = txtDateOfBirth.value;
    const state = txtState.value;
    const age = txtAge.value;

    validatePremiumRuleParameters(dateOfBirth, state, age);

    //Setting queryString parameters
    params = { dateOfBirth: dateOfBirth, state: state, age: age };
    url.search = new URLSearchParams(params).toString();

    //Calling the web api
    fetch(url, {
        method: "GET",
        headers: {
            "Accept": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => {
            if (data.premium) {
                txtPremiumValue.value = data.premium;
                setPeriodAmounts();
            }
            else {
                clearControls();
                handleError(data);
            }                
    })
    .catch(error => handleError('There was an error processing the request: ' + error));
}

function validatePremiumRuleParameters(dateOfBirth, state, age) {
    if (!validateDateFormat(dateOfBirth))
        handleError('Invalid date format');
    if (!validateAgeFormat(age))
        handleError('Invalid age format');
    if (!validateAgeWithBirthDate(dateOfBirth, parseInt(age)))
        handleError('Age does not match birth date');
    if (!validateStateFormat(state))
        handleError('Invalid state code format');
}

//Validate the format of the dateString (YYYY-MM-DD)
function validateDateFormat(dateString) {    
    const dateformat = /[0-9]{4}-(0[1-9]|1[0-2])-(0[1-9]|[1-2][0-9]|3[0-1])/; //YYYY-MM-DD Regex
    return dateString && dateString.match(dateformat);
}

function validateAgeFormat(age) {
    return age && !isNaN(age);
}

//Validate if the age matches the birthdate age 
function validateAgeWithBirthDate(dateOfBirth, age) {
    return getAgeFromDate(dateOfBirth) !== age;
}

//Validate the format of the US state code
function validateStateFormat(state) {
    return state && state.length === 2;
}


//Set the payment amount depending of the payment frequency selected for each of the periods (Currently Annually and Monthly)
//The frequency values are configured in a constant dictionary in the /common/constant file in case new frequencies needs to be added
function setPeriodAmounts() {
    const txtPremiumValue = document.getElementById('txtPremiumValue');
    if (txtPremiumValue.value) {
        const premiumAmount = parseFloat(txtPremiumValue.value);    
        const frequencyCode = document.getElementById('dropFrequency').value;
        let frequencyObject = Object.values(Frequency).find(f => f.Code === frequencyCode)

        //The values for the annually and monthly periods are calculated and set
        //In case more periods are added, the setAmount function needs to be called for those periods and set the new input element value
        if (frequencyObject) {
            const payments = frequencyObject.Payments;
            setAmount(premiumAmount, 'txtAnnuallyValue', payments, Frequency.Annually.Payments);
            setAmount(premiumAmount, 'txtMonthlyValue', payments, Frequency.Monthly.Payments);
        } else {
            handleError('No Payment info configured');
        }  
    }  
}

function setAmount(premiumAmount, inputId, payments, paymentFrequency) {
    let inputElement = document.getElementById(inputId);
    inputElement.value = premiumAmount * payments / paymentFrequency;    
}

//Setting the age input value when the date is selected, only if its a valid date
function setAge() {
    const txtDateOfBirth = document.getElementById('txtDateOfBirth');
    if (validateDateFormat(txtDateOfBirth.value)) {
        const birthDate = Date.parse(txtDateOfBirth.value)
        const age = getAgeFromDate(birthDate);
        if (age >= 0) {
            let txtPremiumValue = document.getElementById('txtAge');
            txtPremiumValue.value = age;
        } else {
            handleError('Please enter a valid date');
        }        
    }           
}

function getAgeFromDate(date) {
    const dateDiff = Date.now() - date;
    const age = new Date(dateDiff).getUTCFullYear() - 1970;
    return age
}

function clearControls() {
    let txtPremiumValue = document.getElementById('txtPremiumValue');
    let txtAnnuallyValue = document.getElementById('txtAnnuallyValue');
    let txtMonthlyValue = document.getElementById('txtMonthlyValue');

    txtPremiumValue.value = '';
    txtAnnuallyValue.value = '';
    txtMonthlyValue.value = '';
}

function handleError(errorMessage) {
    console.log(errorMessage);
    alert(errorMessage);
}