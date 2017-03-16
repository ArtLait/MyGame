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
    camera = new THREE.PerspectiveCamera(45
       , window.innerWidth / window.innerHeight, 0.1, 1000);
    //camera = new THREE.OrthographicCamera(window.innerWidth / -2,
    //    window.innerWidth / 2, window.innerHeight / 2, window.innerHeight / -2, 1, 1000)
   // var controls = new THREE.OrbitControls(camera);
  // controls.addEventListener('change', render);
    renderer = new THREE.WebGLRenderer();
    renderer.setClearColorHex(0xEEEEEE);
    renderer.setSize(window.innerWidth, window.innerHeight);
    axes = new THREE.AxisHelper(20);
    scene.add(axes);
    planeGeometry = new THREE.PlaneGeometry(1, 1, 1, 1);
    planeMaterial = new THREE.MeshBasicMaterial({ color: 0xcccccc });
    plane = new THREE.Mesh(planeGeometry, planeMaterial);
    plane.position.set(0, 0, -1);
    plane.scale.set(100, 100, 100);
    scene.add(plane);

    /*var spotLight = new THREE.SpotLight(0xffffff);
    spotLight.position.set(100, 0, 40);
    scene.add(spotLight);*/
    //var cubeGeometry = new THREE.CubeGeometry(40, 40, 4);
    //var cubeMaterial = new THREE.MeshLambertMaterial(
    //    { color: 0xff0000 });
    //var cubeTest = new THREE.Mesh(cubeGeometry, cubeMaterial);
    //scene.add(cubeTest);

    //var spriteMap = new THREE.ImageUtils.loadTexture("/Content/img/englishFlag.png");
    //var spriteMaterial = new THREE.SpriteMaterial({ map: spriteMap });
    //var sprite = new THREE.Sprite(spriteMaterial);
    //spriteMaterial.map.needsUpdate = true;
    //sprite.scale.set(50, 50, 1);
    //scene.add(sprite);    

   // sphereGeometry = new THREE.SphereGeometry(20, 20, 20);
   // sphereMaterial = new THREE.MeshPhongMaterial(
   //     { map: THREE.ImageUtils.loadTexture("../../Content/img/pacman.jpg") });
   //// sphereMaterial.map.needsUpdate = true;
   // sphere = new THREE.Mesh(sphereGeometry, spriteMap);   
   // scene.add(sphere);

    var curve = new THREE.EllipseCurve(
	0, 0,            // ax, aY
	10, 2,           // xRadius, yRadius
	0, 2 * Math.PI,  // aStartAngle, aEndAngle
	false,            // aClockwise
	0             // aRotation
);

    var path = new THREE.Path(curve.getPoints(50));
    var geometry = path.createPointsGeometry(50);
    var material = new THREE.LineBasicMaterial({ color: 0xff0000 });

    // Create the final object to add to the scene
    var ellipse = new THREE.Line(geometry, material);
    ellipse.rotation.z = 0;
    scene.add(ellipse);
    //console.log(window.innerWidth/2, window.innerHeight/2);
    //var centerMap = {
    //    x: window.innerWidth / 2 + ellipse.position.x,
    //    y: window.innerHeight / 2 + ellipse.position.y
    //};
    var centerMap = {
        x: window.innerWidth / 2,
        y: window.innerHeight / 2
    };   
    //$("body").click(function (e) {
        
    //    var currentPoint = {
    //        x: e.pageX,
    //        y: e.pageY
    //    };        
    //    var catheterX = currentPoint.x - centerMap.x;
    //    var catheterY = currentPoint.y - centerMap.y;
    //    var tangens = catheterY / catheterX * (-1);    
    //    var angel = Math.atan(tangens);
    //    var degree = angel / Math.PI * 180;
  
    //    if (catheterX > 0) {
    //        var dx = Math.cos(Math.abs(angel)) * 10;
    //    }
    //    else {
    //        var dx = -Math.cos(Math.abs(angel)) * 10;
    //    }
    //    if (catheterY > 0) {
    //        var dy = -Math.sin(Math.abs(angel)) * 10;
    //    }
    //    else {
    //        var dy = +Math.sin(Math.abs(angel)) * 10;
    //    }        
    //    camera.position.x += dx;
    //    camera.position.y += dy;
    //    ellipse.position.x += dx;
    //    ellipse.position.y += dy;
    //    ellipse.rotation.z = angel;
         
    //    render();
    //});

    //$("body").mousemove(function (e) {

    //    console.log(i++);
       
    //}); 
    camera.position.set(0, 0, 400);
    camera.lookAt(scene.position);
    $("#myGame").append(renderer.domElement);
    $("#myGame").find("canvas").addClass("absolute")
        .attr('id', 'mainCanvas').attr('tabindex', '1');
    renderer.render(scene, camera);
    window.addEventListener('resize', onWindowResize, false);
}
function createRectangle(players, x, y, sizeX, sizeY, color) {

    var curve = new THREE.EllipseCurve(
	    x, y,            // ax, aY
	    sizeX, sizeY,           // xRadius, yRadius
	    0, 2 * Math.PI,  // aStartAngle, aEndAngle
	    false,            // aClockwise
	    0                 // aRotation
    );

    var path = new THREE.Path(curve.getPoints(50));
    var geometry = path.createPointsGeometry(50);
    var material = new THREE.LineBasicMaterial({ color: color });

    // Create the final object to add to the scene
    var cube = new THREE.Line(geometry, material);
    scene.add(cube);        
    //render();
    return cube;
}
//function createRectangle(players, x, y, sizeX, sizeY, color) {
    
//    var cubeGeometry = new THREE.CubeGeometry(sizeX, sizeY, 4);
//    var cubeMaterial = new THREE.MeshBasicMaterial(
//        { color: color, wireframe: true });
//    var cube = new THREE.Mesh(cubeGeometry, cubeMaterial);
//    cube.position.x = x;
//    cube.position.y = y;
//    cube.position.z = 0;
//    scene.add(cube);
//    render();
//    return cube;
//}
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