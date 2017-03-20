init();
//animate();
var container;
var camera, controls, scene, renderer;
var planeGeometry, planeMaterial, plane, sphere,
    cubeGeometry, cubeMaterial, cube,
    sphereGeometry, sphereMaterial, sphere;
var cross;
var players = [];
var someFoodArray = [];
var connectionId;

function init() {    
    scene = new THREE.Scene();
    camera = new THREE.OrthographicCamera(window.innerWidth / -2,
        window.innerWidth / 2, window.innerHeight / 2, window.innerHeight / -2, 1, 1000)
    renderer = new THREE.WebGLRenderer();
    renderer.setClearColorHex(0xEEEEEE);
    renderer.setSize(window.innerWidth, window.innerHeight);
    axes = new THREE.AxisHelper(20);
    scene.add(axes);
    var spriteMap = new THREE.TextureLoader().load("/Content/img/pacman.png");
    var spriteMaterial = new THREE.SpriteMaterial({ map: spriteMap, color: 0xffffff });
    var sprite = new THREE.Sprite(spriteMaterial);
    scene.add(sprite);    
    planeGeometry = new THREE.PlaneGeometry(1, 1, 1, 1);
    planeMaterial = new THREE.MeshBasicMaterial({ color: 0xcccccc });
    plane = new THREE.Mesh(planeGeometry, planeMaterial);
    plane.position.set(0, 0, -1);
    plane.scale.set(100, 100, 100);
    scene.add(plane);
    camera.position.set(0, 0, 400);
    camera.lookAt(scene.position);
    $("#myGame").append(renderer.domElement);
    $("#myGame").find("canvas").addClass("absolute")
        .attr('id', 'mainCanvas').attr('tabindex', '1');
    renderer.render(scene, camera);
    window.addEventListener('resize', onWindowResize, false);
}
function createRectangle(players, x, y, sizeX, sizeY, color) {

    var spriteMap = new THREE.ImageUtils.loadTexture("/Content/img/pacman.png");
    var spriteMaterial = new THREE.SpriteMaterial({ map: spriteMap, color: color });
    var cube = new THREE.Sprite(spriteMaterial);
    spriteMaterial.map.needsUpdate = true;    
    cube.position.x = x;
    cube.position.y = y;
    cube.scale.set(sizeX, sizeY, 1);
    
    scene.add(cube);    
    return cube;
}
function createSomeFood(x, y, sizeX, sizeY, color) {
    
    var spriteMap = new THREE.ImageUtils.loadTexture("/Content/img/pacman.png");
    var spriteMaterial = new THREE.SpriteMaterial({ map: spriteMap, color: color});
    var cube = new THREE.Sprite(spriteMaterial);
    spriteMaterial.map.needsUpdate = true;    
    cube.position.x = x;
    cube.position.y = y;
    cube.scale.set(sizeX, sizeY, 1);

    scene.add(cube);
    return cube;
}
function animate() {
    sphere.rotation.x += 2 * Math.PI / 100;
    requestAnimationFrame(animate);
    render();
}

function onWindowResize() {

    camera.aspect = window.innerWidth / window.innerHeight;
    camera.updateProjectionMatrix();

    renderer.setSize(window.innerWidth, window.innerHeight);

    render();
}
function render() {

    renderer.render(scene, camera);
};