import "dart:html" as window;
import "package:angular/angular.dart";
import "package:angular_router/angular_router.dart";
import "package:angular_forms/angular_forms.dart";

import "../../formats/box.dart";
import "../../../services/box_service.dart";
//import "../services/drawing_service.dart";
import "../../route_paths.dart" as paths;

@Component(
  selector: "my-blackbox",
  templateUrl: "blackbox_component.html",
  directives: [coreDirectives, routerDirectives],
  styleUrls: ["blackbox_component.css"],
)
class BlackBoxComponent implements OnInit {
  List<String> allInputs = List<String>();
  List<String> boxAdverbs = List<String>();
  List<String> boxVerbs = List<String>();

  String mode = "";

  String warning = "";

  String activeInput = "";
  String activeAdverb = "";
  String activeVerb = "";

  final BoxService _boxService;
  //final DrawingService _drawingService;

  BlackBoxComponent(this._boxService);

  //Input selection methods.
  void onInputSelect(String input) {
    activeInput = input;
  }

  void onAdverbSelect(String adverb) {
    activeAdverb = adverb;
  }

  void onVerbSelect(String verb) {
    activeVerb = verb;
  }


  //Input submission methods.
  void onSubmitInput() {
    if (activeInput == "") {
      warning = "Please select an input!";
      return;
    }

    //Show line guides.
    mode = "input";
  }

  void onCommitInput() {

  }

  void onSubmitBox() {
    if (activeAdverb == "" || activeVerb == "") {
      warning = "Please select both a verb and adverb!";
      return;
    }

    //Show box guides.
    mode = "box";
  }

  void onCommitBox() {
  
  }

  void onSelectEdge(dynamic event) {
    var div = window.document.getElementById("canvas").getBoundingClientRect();

    print("Normalized point: (" + ((event.page.x - div.topLeft.x)/div.width).toString() + "," + ((event.page.y - div.topLeft.y)/div.height).toString() + ")");

    //print(window.document.getElementById("canvas").getBoundingClientRect().topLeft.x.toString());
  }

  //Alert methods.
  void onClearAlert() {
    warning = "";
  }

  void onClearSelection() {
    activeInput = "";
    activeVerb = "";
    activeAdverb = "";

    mode = "";
  }

  //String projectUrl(String id) => paths.RoutePaths.project.toUrl(parameters: {paths.idParam: "$id"});

  @override
  void ngOnInit() async {
    boxAdverbs = (await _boxService.getAllAdverbs());
    boxVerbs = (await _boxService.getAllVerbs());
    //edgeGuides = (await _drawingService.getEdgeGuides());
    //innerGuides = (await _drawingService.getInnerGuides());

    allInputs.add("test1");
    allInputs.add("test2");
  }
}