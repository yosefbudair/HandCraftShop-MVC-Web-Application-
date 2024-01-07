var picPaths = ["~/as/images/Welcome4.png", "~/as/images/Welcome3.png" ];
var imageIndex = 0;
var Image;

// An event callback for starting the interval
function startInterval() {
    setInterval(function() {
        Image.src = picPaths[imageIndex];
        if (imageIndex === (picPaths.length - 1)) {
            imageIndex = 0;
        }
        else {  
            imageIndex = imageIndex + 1; // It can be replaced with imageIndex ++  
        }
    }


        , 3000); // 3s mean
}



window.onload = function () {
    Image = document.getElementById('images');
    startInterval();
}

function Over(element) {
    
   
    element.style.fontSize = "2rem";
    
}
function Out(element) {
    element.style.fontSize = "1.25rem";
}


function change1() {

    document.getElementById("card1").src = "images/Thailand-done.jpg";
    s

}

function change2() {

    document.getElementById("card2").src = src = "images/Varenna,Italy-done.jpg";


}

function change3() {

    document.getElementById("card3").src = "images/Agra,Uttar Pradesh,India-done.jpg";


}

function change4() {

    document.getElementById("card4").src = "images/Louvre,Museum-done.jpg";


}

function change5() {

    document.getElementById("card5").src = "images/BridgeofPeace, Tbilisi,Georgia-done.jpg";


}
