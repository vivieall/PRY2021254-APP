<?php
$connectionPass = $_POST ['ConectionPass'];
$os = $_POST ['os'];
$plataformpass = $_POST ['plataformpass'];

if ($connectionPass == "Lx!537u^h?vnb#?"){
if ($os == "android" && $plataformpass != "d8RXq+bE@mXcm3"){
  $data = array('done' => false, 'message' => 'Autentificacion Fallida');
  Header('Content-Type: application/json');
  echo json_encode($data);
  exit();
}

else if ($os == "ios" && $plataformpass != "5y=ARb7th+fPdWm"){
  $data = array('done' => false, 'message' => 'Autentificacion Fallida');
  Header('Content-Type: application/json');
  echo json_encode($data);
  exit();
}else if ($os == "windows" && $plataformpass != "Ys*xrUz73%#vPV?"){
  $data = array('done' => false, 'message' => 'Autentificacion Fallida');
  Header('Content-Type: application/json');
  echo json_encode($data);
  exit();
}else if ($os == "ps4" && $plataformpass != "fRTb4nd?gM^GBP="){
  $data = array('done' => false, 'message' => 'Autentificacion Fallida');
  Header('Content-Type: application/json');
  echo json_encode($data);
  exit();
}else if ($os == "xboxone" && $plataformpass != "G=yZ2RN3Jguw8X"){
  $data = array('done' => false, 'message' => 'Autentificacion Fallida');
  Header('Content-Type: application/json');
  echo json_encode($data);
  exit();
}else if ($os == "wii" && $plataformpass != "ZAXn8-_?vsKumTG"){
  $data = array('done' => false, 'message' => 'Autentificacion Fallida');
  Header('Content-Type: application/json');
  echo json_encode($data);
  exit();
}else if ($os == "osx" && $plataformpass != "LGn!6VgXse-VT7x"){
  $data = array('done' => false, 'message' => 'Autentificacion Fallida');
  Header('Content-Type: application/json');
  echo json_encode($data);
  exit();
}else if ($os == "linux" && $plataformpass != "bsw?QFw+h!zQ9Q"){
  $data = array('done' => false, 'message' => 'Autentificacion Fallida');
  Header('Content-Type: application/json');
  echo json_encode($data);
  exit();
}
}
?>
