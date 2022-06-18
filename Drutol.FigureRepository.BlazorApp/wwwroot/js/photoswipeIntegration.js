import PhotoSwipeLightbox from "/photoswipe/photoswipe-lightbox.esm.js";

function openLightBox(imageArray, index) {
    const options = {
        dataSource: imageArray,
        pswpModule: () => import("/photoswipe/photoswipe.esm.js")
    };
    console.log(imageArray);
    const lightbox = new PhotoSwipeLightbox(options);
    lightbox.loadAndOpen(index);
}

globalThis.openLightBox = openLightBox;