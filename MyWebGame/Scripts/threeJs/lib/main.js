﻿var scene = new THREE.Scene();
var camera = new THREE.PerspectiveCamera(45
    , window.innerWidth / window.innerHeight, 0.1, 1000);
//var controls = new THREE.OrbitControls(camera);
//controls.addEventListener('change', render);
var renderer = new THREE.WebGLRenderer();
renderer.setClearColorHex(0xEEEEEE);
renderer.setSize(window.innerWidth, window.innerHeight);
var axes = new THREE.AxisHelper(20);
scene.add(axes);
var planeGeometry = new THREE.PlaneGeometry(60, 20, 1, 1);
var planeMaterial = new THREE.MeshBasicMaterial({ color: 0xcccccc });
var plane = new THREE.Mesh(planeGeometry, planeMaterial);
plane.rotation.x = -0.5 * Math.PI;
plane.position.x = 15;
plane.position.y = 0;
plane.position.z = 0;
scene.add(plane);
var cubeGeometry = new THREE.CubeGeometry(4, 4, 4);
var cubeMaterial = new THREE.MeshBasicMaterial(
    { color: 0xff0000, wireframe: true });
var cube = new THREE.Mesh(cubeGeometry, cubeMaterial);
cube.position.x = -4;
cube.position.y = 3;
cube.position.z = 0;
scene.add(cube);
var sphereGeometry = new THREE.SphereGeometry(4, 20, 20);
var sphereMaterial = new THREE.MeshBasicMaterial(
    { color: 0x7777ff, wireframe: true });
var sphere = new THREE.Mesh(sphereGeometry, sphereMaterial);
sphere.position.x = 20;
sphere.position.y = 4;
sphere.position.z = 2;
scene.add(sphere);
camera.position.x = -30;
camera.position.y = 40;
camera.position.z = 30;
camera.lookAt(scene.position);
$("#myGame").append(renderer.domElement);
$("#myGame").find("canvas").addClass("absolute");
renderer.render(scene, camera);

window.addEventListener('resize', onWindowResize, false);
function animate() {
    sphere.rotation.x += 2 * Math.PI / 100;
    requestAnimationFrame(animate);
      renderer.render(scene, camera);
    controls.update();
};
//animate();
function onWindowResize() {

    camera.aspect = window.innerWidth / window.innerHeight;
    camera.updateProjectionMatrix();

    renderer.setSize(window.innerWidth, window.innerHeight);

    render();
}
function render() {

    renderer.render(scene, camera);
}