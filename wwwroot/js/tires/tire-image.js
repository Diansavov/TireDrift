function triggerFileUpload() {
    document.getElementById('image-input').click();
}
function handleFileUpload(event) {
    var file = event.target.files[0];
    
    if (file) {
        var reader = new FileReader();
        reader.onload = function (e){
            var image = document.getElementById('image');
            image.src = e.target.result;
        }
        reader.readAsDataURL(file);                                              
    }
}
document.getElementById('image-link').addEventListener('click', triggerFileUpload)
document.getElementById('image-input').addEventListener('change', handleFileUpload)