// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

window.onload = function () {

    const typeName = document.getElementById('typeName');
    const typeCardNumber = document.getElementById('typeCardNumber');
    const typeDateExp = document.getElementById('typeDateExp');
    const typeCvv = document.getElementById('typeCvv');

// CREDIT CARD IMAGE JS
    document.querySelector('.preload').classList.remove('preload');
    document.querySelector('.creditcard').addEventListener('click', function () {
        if (this.classList.contains('flipped')) {
            this.classList.remove('flipped');
        } else {
            this.classList.add('flipped');
        }
    })

//On Input Change Events
    typeName.addEventListener('input', function () {
        if (typeName.value.length == 0) {
            document.getElementById('svgname').innerHTML = 'Ivan Ivanov';
            document.getElementById('svgnameback').innerHTML = 'Ivan Ivanov';
        } else {
            document.getElementById('svgname').innerHTML = this.value;
            document.getElementById('svgnameback').innerHTML = this.value;
        }
    });

    typeCardNumber.addEventListener('input', function () {
        if (typeCardNumber.value.length==0) {
            document.getElementById('svgnumber').innerHTML = '0123 4567 8910 1112';
        } else {
            document.getElementById('svgnumber').innerHTML = this.value;
        }
    });

    typeDateExp.addEventListener('input', function () {
        if (typeDateExp.value.length == 0) {
            document.getElementById('svgexpire').innerHTML = '01/24';
        } else {
            document.getElementById('svgexpire').innerHTML = this.value;
        }
    });

    typeCvv.addEventListener('input', function () {
        if (typeCvv.value.length == 0) {
            document.getElementById('svgsecurity').innerHTML = '000';
        } else {
            document.getElementById('svgsecurity').innerHTML = this.value;
        }
    });

//On Focus Events
    typeName.addEventListener('focus', function () {
        document.querySelector('.creditcard').classList.remove('flipped');
    });

    typeCardNumber.addEventListener('focus', function () {
        document.querySelector('.creditcard').classList.remove('flipped');
    });

    typeDateExp.addEventListener('focus', function () {
        document.querySelector('.creditcard').classList.remove('flipped');
    });

    typeCvv.addEventListener('focus', function () {
        document.querySelector('.creditcard').classList.add('flipped');
    });
};
                           