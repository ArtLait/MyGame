init();
//animate();
var container;
var camera, controls, scene, renderer;
var planeGeometry, planeMaterial, plane, sphere,
    cubeGeometry, cubeMaterial, cube,
    sphereGeometry, sphereMaterial, sphere;
var cross;
var players = [];

function init() {
    scene = new THREE.Scene();
    //camera = new THREE.PerspectiveCamera(45
    //   , window.innerWidth / window.innerHeight, 0.1, 1000);
    camera = new THREE.OrthographicCamera(window.innerWidth / -2, window.innerWidth / 2, window.innerHeight / 2, window.innerHeight / -2 , 1, 1000)
    console.log(camera);
    //var controls = new THREE.OrbitControls(camera);
    //controls.addEventListener('change', render);
    renderer = new THREE.WebGLRenderer();
    renderer.setClearColorHex(0xEEEEEE);
    renderer.setSize(window.innerWidth, window.innerHeight);
    axes = new THREE.AxisHelper(20);
    scene.add(axes);
    planeGeometry = new THREE.PlaneGeometry(1, 1, 1, 1);
    planeMaterial = new THREE.MeshBasicMaterial({ color: 0xcccccc });
    plane = new THREE.Mesh(planeGeometry, planeMaterial);
    plane.position.x = 0;
    plane.position.y = 0;
    plane.position.z = 30;
    scene.add(plane);
    cubeGeometry = new THREE.CubeGeometry(4, 4, 4);
    cubeMaterial = new THREE.MeshBasicMaterial(
        { color: 0xff0000, wireframe: true });
    cube = new THREE.Mesh(cubeGeometry, cubeMaterial);
    cube.position.x = -40;
    cube.position.y = 3;
    cube.position.z = 0;
    scene.add(cube);
    sphereGeometry = new THREE.SphereGeometry(4, 20, 20);
    sphereMaterial = new THREE.MeshBasicMaterial(
        { color: 0x7777ff, wireframe: true });
    sphere = new THREE.Mesh(sphereGeometry, sphereMaterial);
    sphere.position.x = 20;
    sphere.position.y = 4;
    sphere.position.z = 2;
    scene.add(sphere);
    camera.position.x = 0;
    camera.position.y = 0;
    camera.position.z = 0;
    camera.lookAt(scene.position);
    $("#myGame").append(renderer.domElement);
    $("#myGame").find("canvas").addClass("absolute")
        .attr('id', 'mainCanvas').attr('tabindex', '1');
    renderer.render(scene, camera);
    window.addEventListener('resize', onWindowResize, false);
}
function createRectangle(players, x, y) {

    cubeGeometry = new THREE.CubeGeometry(4, 4, 4);
    cubeMaterial = new THREE.MeshBasicMaterial(
        { color: 0xff0000, wireframe: true });
    var cube = new THREE.Mesh(cubeGeometry, cubeMaterial);
    cube.position.x = x;
    cube.position.y = y;
    cube.position.z = 0;
    scene.add(cube);
    render();
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