function togglePasswordVisibility() {
    var passwordInput = document.getElementById('password');
    var toggleIcon = document.getElementById('toggleIcon');
    if (passwordInput.type === 'password') {
        passwordInput.type = 'text';
        toggleIcon.classList.remove('fa-eye');
        toggleIcon.classList.add('fa-eye-slash');
    } else {
        passwordInput.type = 'password';
        toggleIcon.classList.remove('fa-eye-slash');
        toggleIcon.classList.add('fa-eye');
    }
}

// Ensure the icon state is preserved on focus and blur
document.getElementById('password').addEventListener('focus', function () {
    var toggleIcon = document.getElementById('toggleIcon');
    toggleIcon.style.display = 'block';
});

document.getElementById('password').addEventListener('blur', function () {
    var toggleIcon = document.getElementById('toggleIcon');
    toggleIcon.style.display = 'block';
});

// Ensure the icon is always visible
document.addEventListener('DOMContentLoaded', function () {
    var toggleIcon = document.getElementById('toggleIcon');
    toggleIcon.style.display = 'block';
});
