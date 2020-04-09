function getRandomSize(min, max) {
    return Math.round(Math.random() * (max - min) + min);
}


export default function getImages(total) {
    let images = []
    for (var i = 0; i < total; i++) {
        var width = getRandomSize(200, 400);
        var height = getRandomSize(200, 400);
        images = images.concat(`https://placekitten.com/${width}/${height}`);
    }
    return images;
}