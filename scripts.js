document.getElementById('cipherType').addEventListener('change', function () {
    const cipherType = this.value;
    const shiftGroup = document.getElementById('shiftGroup');
    const keyGroup = document.getElementById('keyGroup');

    if (cipherType === 'caesar') {
        shiftGroup.style.display = 'block';
        keyGroup.style.display = 'none';
    } else if (cipherType === 'playfair') {
        shiftGroup.style.display = 'none';
        keyGroup.style.display = 'block';
    } else {
        shiftGroup.style.display = 'none';
        keyGroup.style.display = 'none';
    }
});

document.getElementById('doOperation').addEventListener('click', function () 
{
    const cipherType = document.getElementById('cipherType').value;
    const operation = document.getElementById('operation').value;
    const plainText = document.getElementById('plainText').value;
    const shift = document.getElementById('shift').value;
    const key = document.getElementById('key').value;

    let url = 'https://localhost:7193';
    let _body = {};

    if (cipherType === 'caesar') 
    {
        url += `/api/Cipher/caesar/${operation}`;
        _body = { plainText: plainText, shift: shift };
    } 
    else if (cipherType === 'playfair') 
    {
        url += `/api/Cipher/playfair/${operation}`;
        _body = { plainText: plainText, key: key };
    } 
    else if (cipherType === 'monoalphabetic') 
    {
        url += `/api/Cipher/monoalphabetic/${operation}`;
        _body = { plainText: plainText };
    }

    const myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");    
    
    const requestOptions = {
        method: "POST",
        headers: myHeaders,
        body: JSON.stringify(_body),  
        redirect: "follow"
      };
      
    
    fetch(url, requestOptions)
    .then(response => response.json())
    .then(data => 
    {
        const resultText = document.getElementById('resultText');
        resultText.textContent = data.response;
    }).catch(error => {
        // console.error("Fetch Error:", error);
        document.getElementById('resultText').textContent = error; //"Error: Unable to process request.";
    });
});