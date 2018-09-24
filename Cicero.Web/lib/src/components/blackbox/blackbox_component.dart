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
  List<String> boxAdverbs = List<String>();
  List<String> boxVerbs = List<String>();

  final BoxService _boxService;
  //final DrawingService _drawingService;

  BlackBoxComponent(this._boxService);

  //String projectUrl(String id) => paths.RoutePaths.project.toUrl(parameters: {paths.idParam: "$id"});

  @override
  void ngOnInit() async {
    boxAdverbs = (await _boxService.getAllAdverbs());
    boxVerbs = (await _boxService.getAllVerbs());
    //edgeGuides = (await _drawingService.getEdgeGuides());
    //innerGuides = (await _drawingService.getInnerGuides());
  }
}